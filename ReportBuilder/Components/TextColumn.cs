namespace HtmlReport.ReportBuilder.Components;

public class TextColumn : Column
{
    public TextColumn(Row row, string title, string value):base(row)
    {
        Children.Add(new Text(title, value));
    }
}
