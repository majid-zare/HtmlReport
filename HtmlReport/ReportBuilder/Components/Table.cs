using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class Table : BaseComponent
{
    public TableRow Header { get; set; }

    public override void Render(StringBuilder sb)
    {
        sb.Append($"<table {RenderCssClasses("table")}>");
        if (Header != null)
        {
            sb.Append("<thead>");
            Header.Render(sb);
            sb.Append("</thead>");
        }

        sb.Append("<tbody>");
        foreach (var row in Children)
        {
            row.Render(sb);
        }
        sb.Append("</tbody>");

        sb.Append("</table>");
    }
}
