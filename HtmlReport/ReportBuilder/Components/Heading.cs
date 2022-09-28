using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public enum HeadingSize
{
    H1 = 1,
    H2 = 2,
    H3 = 3,
    H4 = 4,
    H5 = 5,
    H6 = 6
}

public class Heading : BaseComponent
{
    public string Content { get; set; }
    public HeadingSize Size { get; set; }

    public override void Render(StringBuilder sb)
    {
        sb.Append($"<h{(byte)Size} class=\"{RenderCssClasses()}\">{Content}</h{(byte)Size}>");
    }
}
