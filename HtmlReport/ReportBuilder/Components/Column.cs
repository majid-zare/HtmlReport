using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public enum TextAlign
{
    Center,
    Right, 
    Left
}

public class Column : BaseComponent
{
    public Row Row { get; private set; }
    public Column(Row row)
    {
        Row = row;
    }

    public override void Render(StringBuilder sb)
    {
        sb.Append($"<div {RenderCssClasses("col", $"col-{GetColumnSize()}")}>");
        RenderChildren(sb);
        sb.Append("</div>");
    }

    private string GetColumnSize()
    {
        return Row.Children.Count switch
        {
            1 => "12",
            2 => "6",
            3 => "4",
            4 => "3",
            _ => String.Empty
        };
    }
}
