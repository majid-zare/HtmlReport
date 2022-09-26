using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class Row: BaseComponent
{
    public override void Render(StringBuilder sb)
    {
        sb.Append($"<div class=\"row {RenderCssClasses()}\">");
        RenderChildren(sb);
        sb.Append("</div>");
    }
}
