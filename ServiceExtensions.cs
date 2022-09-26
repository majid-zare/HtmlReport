using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlReport
{
    public static class ServiceExtensions
    {
        public static void AddHtmlReportService(IServiceCollection services, List<string> assetsPath)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton(x => new HtmlToPdfExporter(x.GetService<IConverter>(), assetsPath));
        }
    }
}
