using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public class Text : BaseComponent
{
    public string? Label { get; }
    public string Value { get; }

    public Text(string label, string value)
    {
        Label = label;
        Value = value;
    }

    public Text(string value)
    {
        Label = null;
        Value = value;
    }

    public override void Render(StringBuilder sb)
    {
        sb.Append(String.IsNullOrWhiteSpace(Label) ? Value : $"<b class=\"label\">{Label}:</b>{Value}");
    }
}
