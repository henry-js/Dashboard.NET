using Terminal.Gui;

namespace Dashboard.NET.Cli.UI;

public class DashPanel : PanelView
{
    public DashPanel(View child) : base(child)
    {
        LayoutStyle = LayoutStyle.Computed;
    }

    public DashPanel() : base()
    {
        LayoutStyle = LayoutStyle.Computed;
    }
}
