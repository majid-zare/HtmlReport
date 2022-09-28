using DinkToPdf;
using HtmlReport;
using HtmlReport.ReportBuilder;
using HtmlReport.ReportBuilder.Components;

var converter = new SynchronizedConverter(new PdfTools());
var assetsPath = new List<string>
{
    $"{Environment.CurrentDirectory}/assets/global.css",
    $"{Environment.CurrentDirectory}/assets/jquery.js"
};

var htmlToPdfExporter = new HtmlToPdfExporter(converter, assetsPath);

var report = new HtmlReport.ReportBuilder.Components.HtmlReport();

var testData = new List<ReportData>
{
    new("Majid", "Zare"),
    new("Majid", "Zare"),
    new("Majid", "Zare"),
};


var headerTable = new Table();
headerTable.AddRow("col1", "col2", "col3").AddCell(x=> x.Content);


for (int i = 0; i < 10; i++)
{
    var page = new ReportPage();
    page.AddFirstPageHeader(headerTable);

    page.AddHeading("Heading Size H6", HeadingSize.H6);


    var table = page.AddTable()
        .AddSpaceBottom(6)
        .AddHeader("FirstName", "LastName");


    foreach (var item in testData)
    {
        table.AddRow(item.FirstName, item.LastName);
    }

    page.AddRow().AddTextColumn("CurrentUserName", "Mr Test");
    page.AddRow().AddTextColumn("Report Date", DateTime.Now.ToString());
    page.AddRow().AddEmptyColumn().AddColumn(x => x.Content("Sign").TextAlign(TextAlign.Center));

    report.AddPage(page);
}

var bytes = htmlToPdfExporter.RenderHtmlToPdf(report.RenderAsHtml());

File.WriteAllBytes(@"output.pdf", bytes);


internal record ReportData(string FirstName, string LastName);