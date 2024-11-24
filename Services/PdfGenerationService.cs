using amandaReport.Core.Data.Domain;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace amandaReport.Services
{

    public class PdfGenerationService
    {
        private readonly string _contentRootPath;

        public PdfGenerationService(IWebHostEnvironment environment)
        {
            _contentRootPath = environment.ContentRootPath;
        }



        public async Task<string> ConvertHtmlToPdfAsync(string htmlFilePath, string outputPath)
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }  // Additional args for performance
            }))
            {
                var page = await browser.NewPageAsync();

                // Disable unnecessary features like images and CSS
                await page.SetRequestInterceptionAsync(true);
                page.Request += (sender, e) =>
                {
                    if (e.Request.ResourceType == ResourceType.Image || e.Request.ResourceType == ResourceType.StyleSheet)
                    {
                        e.Request.AbortAsync();
                    }
                    else
                    {
                        e.Request.ContinueAsync();
                    }
                };

                var htmlContent = await File.ReadAllTextAsync(htmlFilePath);
                await page.SetContentAsync(htmlContent);
                await page.PdfAsync(outputPath);
            }

            return outputPath;
        }
    }



    //public async Task<string> DataBind(string htmlFilePath, string outputPath, IEnumerable<EventDto> events)
    //{
    //    // Read HTML template from file
    //    var htmlTemplate = await File.ReadAllTextAsync(htmlFilePath);

    //    // Compile Razor template
    //    var resultHtml = Engine.Razor.RunCompile(htmlTemplate, "eventReportTemplate", typeof(IEnumerable<EventDto>), events);

    //    // Save the resulting HTML to a PDF
    //    var browserFetcher = new BrowserFetcher();
    //    await browserFetcher.DownloadAsync(B);

    //    using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
    //    {
    //        var page = await browser.NewPageAsync();
    //        await page.SetContentAsync(resultHtml);
    //        await page.PdfAsync(outputPath);
    //    }

    //    return outputPath;
    //}
}
