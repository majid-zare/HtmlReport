using HtmlReport.ReportBuilder.Components;
using System.Linq.Expressions;

namespace HtmlReport.ReportBuilder;

public static class ReportBuilderExtensions
{
    public static void AddPage(this Components.HtmlReport report, ReportPage page)
    {
        report.Pages.Add(page);
    }

    public static ReportPage AddHeading(this ReportPage page, string value, HeadingSize size)
    {
        var item = new Heading
        {
            Content = value,
            Size = size
        };

        page.Children.Add(item);

        return page;
    }

    public static Row AddRow(this ReportPage page)
    {
        var row = new Row();
        page.Children.Add(row);
        return row;
    }

    public static Row AddRow(this Column column)
    {
        var item = new Row();
        column.Children.Add(item);
        return item;
    }

    public static Column TextAlign(this Column column, TextAlign textAlign)
    {
        column.CssClasses.Add($"text-align-{textAlign.ToString().ToLower()}");
        return column;
    }

    public static Column Content(this Column column, string content)
    {
        column.Children.Add(new Text(content));
        return column;
    }

    public static Column AddImage(this Column column, string base64ImageData, string[]? cssClasses)
    {
        var img = new Image(base64ImageData);
        if (cssClasses != null)
        {
            img.CssClasses.AddRange(cssClasses);
        }
        
        column.Children.Add(img);
        return column;
    }

    public static Row AddColumn(this Row row, Action<Column> configurator)
    {
        var item = new Column(row);
        configurator(item);
        row.Children.Add(item);
        return row;
    }

    public static Row AddEmptyColumn(this Row row)
    {
        var item = new Column(row);
        row.Children.Add(item);
        return row;
    }

    public static Row AddTextColumn(this Row row, string title, string value)
    {
        var item = new TextColumn(row, title, value);
        row.Children.Add(item);
        return row;
    }

    public static Table AddTable(this ReportPage page)
    {
        var item = new Table();
        page.Children.Add(item);
        return item;
    }

    public static T AddSpaceBottom<T>(this T element, byte value) where T : BaseComponent
    {
        element.CssClasses.Add($"mb-{value}");
        return element;
    }

    public static T AddCssClass<T>(this T element, string className) where T : BaseComponent
    {
        element.CssClasses.Add(className);
        return element;
    }

    public static Table AddHeader(this Table table, params string[] headers)
    {
        var item = new TableRow();
        foreach (var header in headers)
        {
            item.Children.Add(new TableHeaderCell(header));
        }

        table.Header = item;
        return table;
    }

    public static TableRow AddRow(this Table table)
    {
        var item = new TableRow();
        table.Children.Add(item);
        return item;
    }

    public static TableRow AddRow(this Table table, params string[] cellValues)
    {
        var item = new TableRow();
        foreach (var value in cellValues)
        {
            item.Children.Add(new TableCell(value));
        }

        table.Children.Add(item);
        return item;
    }

    public static TableRow AddTextCell(this TableRow row, string value)
    {
        var item = new TableCell(value);
        row.Children.Add(item);
        return row;
    }

    public static TableRow AddCell(this TableRow row, Action<TableCell> configurator)
    {
        var item = new TableCell();
        configurator(item);
        row.Children.Add(item);
        return row;
    }


    public static TableCell Content(this TableCell cell, string content)
    {
        cell.Children.Add(new Text(content));
        return cell;
    }

    public static TableCell Content(this TableCell cell, string label, string content)
    {
        cell.Children.Add(new Text(label, content));
        return cell;
    }

    public static TableCell ColSpan(this TableCell cell, byte colSpan)
    {
        cell.ColSpan = colSpan;
        return cell;
    }
}
