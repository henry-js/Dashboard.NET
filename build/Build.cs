using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MinVer;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.Git.GitTasks;
using static Nuke.Common.Tools.MinVer.MinVerTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;
    [Parameter(ValueProviderMember = "Projects")] string Project;
    IEnumerable<string> Projects => Solution.AllProjects.Select(x => x.Name);
    [GitRepository] readonly GitRepository Repository;
    [MinVer] readonly MinVer MinVer;
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath OutputDirectory => RootDirectory / "release";
    AbsolutePath CliDirectory => SourceDirectory / "Cli";
    AbsolutePath TestDirectory => RootDirectory / "tests" / "Dashboard.NET.Tests";


    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            Git("status");
            EnsureExistingDirectory(OutputDirectory);
            EnsureCleanDirectory(OutputDirectory);
            SourceDirectory.GlobDirectories("**/**/bin", "**/**/obj").ForEach(dir => EnsureCleanDirectory(dir));
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            Log.Debug(Configuration);
            DotNetRestore(s => s.SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
            {
                DotNetBuild(settings =>

                    settings.SetProjectFile(CliDirectory)
                                    .EnableNoRestore());
            }
                        );

    Target PrintVersion => _ => _
        .TriggeredBy(Compile)
        .Executes(() =>
        {
            Log.Information("MinVer = {Value}", MinVer.Version);
            Log.Information("main/master branch = {Value}", Repository.IsOnMainOrMasterBranch());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
            DotNetTest(settings =>
                {
                    return settings.SetProjectFile(TestDirectory)
                            .EnableNoRestore()
                            .EnableNoBuild();
                }));

    Target Publish => _ => _
        // .After(Compile)
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPublish(settings =>
            settings.SetProject(CliDirectory)
                    .SetOutput(OutputDirectory)
                    .EnableNoRestore()
                    .EnableNoBuild()
                    );
        });
}
