using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class TableCell : BaseComponent
{
    public byte ColSpan { get; set; }

    public TableCell()
    {
    }

    public TableCell(string content)
    {
        Children.Add(new Text(content));
    }

    public override void Render(StringBuilder sb)
    {
        sb.Append(ColSpan > 0 ? $"<td colspan=\"{ColSpan}\">" : "<td>");
        RenderChildren(sb);
        sb.Append("</td>");
    }
}
