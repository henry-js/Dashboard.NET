using Terminal.Gui;

namespace Dashboard.NET.Cli.UI;
public class Board : Window
{
    public List<DashPanel> Panels { get; set; } = new();

    public NarrowLayout ActiveLayout;
    public Board(string title) : base(title)
    {
        ActiveLayout = new NarrowLayout(4);
        ActiveLayout.InitializeLayout(Panels);
        var label = new Label("Hello");
        Add(label);
        PopulatePanels();
    }

    private void PopulatePanels()
    {
        Panels.ForEach(p => Add(p));
    }
}
