using System.Text;

namespace HtmlReport.ReportBuilder.Components;

public abstract class BaseComponent
{
    internal List<BaseComponent> Children { get; set; } = new();
    internal List<string> CssClasses { get; set; } = new();

    public abstract void Render(StringBuilder sb);

    protected void RenderChildren(StringBuilder sb)
    {
        foreach (var child in Children)
        {
            child.Render(sb);
        }
    }

    protected string RenderCssClasses(params string[] cssClass)
    {
        var result = CssClasses.Union(cssClass).ToArray();
        if (result.Any())
        {
            return $"class=\"{String.Join(" ", result)}\"";
        }

        return String.Empty;
    }
}
