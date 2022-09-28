using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class TableHeaderCell : BaseComponent
{
    public TableHeaderCell(string content)
    {
        Children.Add(new Text(content));
    }

    public override void Render(StringBuilder sb)
    {
        sb.Append("<th>");
        RenderChildren(sb);
        sb.Append("</th>");
    }
}
