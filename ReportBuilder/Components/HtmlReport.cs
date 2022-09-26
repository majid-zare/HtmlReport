using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public enum PaperSize
{
    A4
}

public class HtmlReport
{
    public string Title { get; set; }
    public PaperSize PaperSize { get; set; } = PaperSize.A4;
    public List<ReportPage> Pages { get; set; } = new();

    public string RenderAsHtml()
    {
        var sb = new StringBuilder();
        foreach (var reportPage in Pages)
        {
            reportPage.Render(sb);
        }

        return sb.ToString();
    }
}