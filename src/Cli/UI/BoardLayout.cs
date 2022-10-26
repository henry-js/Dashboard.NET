using Terminal.Gui;

namespace Dashboard.NET.Cli.UI
{
    public interface IBoardLayout
    {
        public List<DashPanel> Apply(List<DashPanel> panels, Dim height, Dim width);
    }
}
