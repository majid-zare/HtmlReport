using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class ReportPage: BaseComponent
{
    private BaseComponent? _firstPageHeader;
    private BaseComponent? _allPageHeader;
    private string? _footerValue;

    public override void Render(StringBuilder sb)
    {
        sb.AppendLine("<div class=\"page\">");
        RenderChildren(sb);
        sb.AppendFormat("<table class=\"footer-section\"><tbody><tr><td>{0}</td><td><span class=\"page-no\"></td></tr></tbody></table>", _footerValue);

        if (_firstPageHeader != null)
        {
            var firstPageHeaderSb = new StringBuilder();
            _firstPageHeader.Render(firstPageHeaderSb);

            sb.AppendFormat("<div class=\"first-page-header\">{0}</div>", firstPageHeaderSb);
        }

        if (_allPageHeader != null)
        {
            var allPageHeaderSb = new StringBuilder();
            _allPageHeader.Render(allPageHeaderSb);

            sb.AppendFormat("<div class=\"all-page-header\">{0}</div>", allPageHeaderSb);
        }

        sb.AppendLine("</div>");
    }

    public void AddFooterText(string value)
    {
        _footerValue = value;
    }

    public void AddFirstPageHeader(BaseComponent element)
    {
        _firstPageHeader = element;
    }

    public void AddFirstPageHeader(string value)
    {
        _firstPageHeader = new Text(value);
    }

    public void AddAllPageHeader(BaseComponent element)
    {
        _allPageHeader = element;
    }
}