using Terminal.Gui;

namespace Dashboard.NET.Cli.UI;
public static class TGui
{
    private static void Initialize() => Application.Init();
    public static void DisplayBoard(IEnumerable<string> panels)
    {
        Initialize();
        var board = new Board("Dashboard");
        Application.Top.Add(board);
        Application.Run();
    }
}
