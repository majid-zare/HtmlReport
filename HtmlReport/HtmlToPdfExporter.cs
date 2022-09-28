using System.Text;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace HtmlReport
{
    public class HtmlToPdfExporter
    {
        private static bool _isWarmedUp;

        private readonly IConverter _pdfConverter;
        private readonly List<string> _assetsPath;
        private readonly string _encoding;

        public HtmlToPdfExporter(IConverter pdfConverter, List<string> assetsPath, string encoding = "utf-8")
        {
            _pdfConverter = pdfConverter;
            _assetsPath = assetsPath;
            _encoding = encoding;
        }

        public byte[] RenderHtmlToPdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    Margins = new MarginSettings { Top = 4, Bottom = 0, Left = 4, Right = 2 },
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        PagesCount = null,
                        HtmlContent = BuildHtmlPage(htmlContent),
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };


            if (!_isWarmedUp)
            {
                _pdfConverter.Convert(doc);
                _isWarmedUp = true;
            }

            return _pdfConverter.Convert(doc);
        }

        private string BuildHtmlPage(string html)
        {
            var sb = new StringBuilder();
            sb.Append(@"<html>");
            sb.Append(@"<head>");
            sb.Append(@"<meta charset=""UTF-8"" />");
            sb.Append(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"">");
            sb.Append(@" <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">");

            foreach (var asset in _assetsPath)
            {
                if (Path.GetExtension(asset).Equals(".css"))
                {
                    sb.Append($@"<link href='file:///{asset}' rel='stylesheet' type='text/css' media='screen'/>");
                }
                else
                {
                    sb.Append($@"<script src='file:///{asset}' ></script>");
                }
            }

            sb.Append(@"</head>");
            sb.Append(@"<body>");
            sb.Append(html);

            sb.Append(@"<script>
function splitPage(index, page, maxHeight) {
    var tmpMaxHeight = maxHeight;
    var firstPageHeader = $('.first-page-header', page);
    var allPageHeader = $('.all-page-header', page);
    var footer = $('.footer-section', page);
    var allPageHeaderHeight = allPageHeader.length > 0 ? allPageHeader.outerHeight(true) : 0;
    var firstPageHeaderHeight = firstPageHeader.length > 0 ? firstPageHeader.outerHeight(true) : 0;
    var footerHeight = footer.length > 0 ? footer.outerHeight(true) : 0;
    allPageHeader = allPageHeader.detach();
    footer = footer.detach();
    firstPageHeader = firstPageHeader.detach();

    var pageItems = [];

    page.children().each(function() {
        var $that = $(this);
        if ($that.is('table')) {
            var cloneTable = $that.clone();
            cloneTable.children('tbody').empty();

            pageItems.push(cloneTable);
            $that.children('tbody').children('tr').each(function() {
                pageItems.push($(this));
            });
        } else {
            pageItems.push($that);
        }
    });

    var $pageWrapper = $('<div></div>');
    var $page = $('<div class=""page""></div>');
    var newTable = null;    

    maxHeight -= (footerHeight + (firstPageHeaderHeight > 0 ? firstPageHeaderHeight : allPageHeaderHeight));
    var currentHeight = 0;
    var hasUncompletedPage = false;
    $.each(pageItems, function(i, pageItem) {
        itemHeight = pageItem.outerHeight(true);

        if (pageItem.hasClass('page-break') || (currentHeight + itemHeight) >= maxHeight) {
            $pageWrapper.append($page);
            maxHeight = tmpMaxHeight - (allPageHeaderHeight + footerHeight);
            currentHeight = 0;

            $page = $('<div class=""page""></div>');

            if (newTable != null) {
                newTable = newTable.clone();
                newTable.find('tbody').empty();
                newTable.appendTo($page);
            }            
        }

        currentHeight += itemHeight;

        if (pageItem.is('table')) {
            newTable = pageItem;
            pageItem.appendTo($page)
        } else {
            if (pageItem.is('tr')) {
                newTable.find('tbody').append(pageItem);
            } else {
                pageItem.appendTo($page)
                newTable = null;
            }
        }

        hasUncompletedPage = true;
    });

    if (hasUncompletedPage){
        $pageWrapper.append($page);
    }
	
    var pages = $pageWrapper.find('.page');
    $.each(pages, function(i, item) {
        var $that = $(item);
        if (i == 0 && firstPageHeader.length) {
            $that.prepend(firstPageHeader.clone());
        } else if (allPageHeader.length) {
            $that.prepend(allPageHeader.clone());
        }

        footer.clone().appendTo($that);
        $that.find('.page-no').text((i + 1) + '/' + pages.length);
    });

    return $pageWrapper;
}

$(function() {
    $('.page').each(function(i, item) {
        var splittedPage = splitPage(i, $(item), 1250);
        $(item).replaceWith(splittedPage);
    });
});
            </script>");

            sb.Append(@"</body>");
            sb.Append(@"</html>");


            File.WriteAllText("output.html", sb.ToString());

            return sb.ToString();
        }
    }
}