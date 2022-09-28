using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class Image : BaseComponent
{
    public string? ImageData { get; }

    public Image(string imageData)
    {
        ImageData = imageData;
    }

    public override void Render(StringBuilder sb)
    {
        sb.Append($"<div class=\"{RenderCssClasses()}\"><img src=\"data:image/png;base64,{ImageData}\" /></div>");
    }
}
