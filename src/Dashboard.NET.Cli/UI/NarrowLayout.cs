using Terminal.Gui;

namespace Dashboard.NET.Cli.UI;

public abstract class NewBaseType
{
    protected NewBaseType(int numberOfPanels)
    {
        Positions = new List<Position>[NumberOfColumns].Select(_ => new List<Position>()).ToArray();
        numberOfPanels = (numberOfPanels % 2 == 0) ? numberOfPanels : numberOfPanels + 1;
        NumberOfPanels = (numberOfPanels > MAXPANELS) ? MAXPANELS : numberOfPanels;
        PanelsPerColumn = numberOfPanels / 2;
        ColumnWidth = 1 / (float)NumberOfColumns * 100;
        ColumnHeight = 1 / (float)PanelsPerColumn * 100;
        for (int column = 0; column < NumberOfColumns; column++)
        {
            for (int row = 0; row < PanelsPerColumn; row++)
            {
                Positions[column].Add(new Position(Pos.Percent(XPos), Pos.Percent(YPos), Dim.Percent(ColumnHeight), Dim.Percent(ColumnWidth)));
                YPos += ColumnHeight;
            }
            YPos = 0;
            XPos += ColumnWidth;
        }
        XPos = 0;
    }
    protected virtual List<Position>[] Positions { get; set; }
    protected virtual float ColumnWidth { get; set; }
    protected virtual float ColumnHeight { get; set; }
    protected virtual int PanelsPerColumn { get; set; }
    protected virtual int MAXPANELS => 6;
    protected virtual int NumberOfPanels { get; set; } = 1;
    public virtual int NumberOfColumns { get; protected set; } = 1;
    protected float XPos;
    protected float YPos;
    public abstract void InitializeLayout(List<DashPanel> panels);
}

public class NarrowLayout : NewBaseType
{
    public override int NumberOfColumns { get; protected set; } = 2;

    public NarrowLayout(int numberOfPanels = 4) : base(numberOfPanels)
    {
    }
    public override void InitializeLayout(List<DashPanel> panels)
    {
        if (panels.Count > 0) throw new ArgumentOutOfRangeException(nameof(panels), "InitializeLayout function should receive an empty List<DashPanel>");
        for (int i = 0; i < Positions.Length; i++)
        {
            foreach (var position in Positions[i])
            {
                panels.Add(new DashPanel(new Label($"Inside Panel Col: {i}, Panel: {Positions[i].IndexOf(position)}"))
                {
                    X = position.X,
                    Y = position.Y,
                    Height = position.Height,
                    Width = position.Width,
                    Border = new Border() { BorderStyle = BorderStyle.Rounded },
                    UsePanelFrame = true
                });
            }
        }
    }

    public void ResetLayout(List<DashPanel> panels) { }

    void CreateLayout(int numberOfPanels)
    {
        for (int i = 0; i <= numberOfPanels; i++)
        {
        }
    }
}
