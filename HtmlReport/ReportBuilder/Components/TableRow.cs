using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class TableRow : BaseComponent
{
    public override void Render(StringBuilder sb)
    {
        sb.Append("<tr>");
        RenderChildren(sb);
        sb.Append("</tr>");
    }
}
