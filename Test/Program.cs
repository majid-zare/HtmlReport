using System.Globalization;
using DinkToPdf;
using HtmlReport;
using HtmlReport.ReportBuilder;
using HtmlReport.ReportBuilder.Components;


const string image = @"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";
var converter = new SynchronizedConverter(new PdfTools());
var assetsPath = new List<string>
{
   Path.Combine(Environment.CurrentDirectory,"assets","global.css"),
   Path.Combine(Environment.CurrentDirectory,"assets","jquery.js")
};

var htmlToPdfExporter = new HtmlToPdfExporter(converter, assetsPath);

var report = new HtmlReport.ReportBuilder.Components.HtmlReport();

var firstPageHeaderTable = new Table();
firstPageHeaderTable.AddRow().AddCells("Cell 1", "Cell 2", "Cell 3").AddCell(x => x.AddImage(image).AddPadding(2, Direction.Left)).AddCells("Cell 5");

var allPageHeaderTable = new Table();
allPageHeaderTable.AddRow().AddCells("AllPageHeader").AddCell(x => x.AddImage(image).AddPadding(2, Direction.Left));


for (int i = 0; i < 5; i++)
{
    var page = new ReportPage();
    page.AddFirstPageHeader(firstPageHeaderTable);
    page.AddAllPageHeader(allPageHeaderTable);

    page.AddHeading("Heading Size H6", HeadingSize.H6);


    var table = page.AddTable()
        .AddMargin(6, Direction.Bottom)
        .AddHeader("FirstName", "LastName");

    for (int j = 0; j < 100; j++)
    {
        table.AddRow().AddCells($"FirstName_{i}", $"LastName_{i}");
    }

    page.AddRow().AddTextColumn("CurrentUserName", "Mr Test");
    page.AddRow().AddTextColumn("Report Date", DateTime.Now.ToString(CultureInfo.InvariantCulture));
    page.AddRow().AddEmptyColumn().AddColumn(x => x.Content("Sign").TextAlign(TextAlign.Center));

    report.AddPage(page);
}

var bytes = htmlToPdfExporter.ExportToPdf(report);

File.WriteAllBytes(@"output.pdf", bytes);

