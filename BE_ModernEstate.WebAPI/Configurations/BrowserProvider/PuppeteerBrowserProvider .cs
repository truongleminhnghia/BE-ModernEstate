
using PuppeteerSharp;

namespace BE_ModernEstate.WebAPI.Configurations.BrowserProvider
{
    public class PuppeteerBrowserProvider : IBrowserProvider
    {
        private readonly Task<IBrowser> _browserTask;

        public PuppeteerBrowserProvider()
        {
            // Khi khởi service, ngay lập tức download + launch browser
            _browserTask = InitializeBrowserAsync();
        }

        private async Task<IBrowser> InitializeBrowserAsync()
        {
            var fetcher = new BrowserFetcher();
            var revision = await fetcher.DownloadAsync();
            return await Puppeteer.LaunchAsync(new LaunchOptions
            {
                ExecutablePath = revision.GetExecutablePath(),
                Headless = true,
                Args = new[]
                {
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--disable-dev-shm-usage",
                    "--disable-gpu"
                }
            });
        }

        public Task<IBrowser> GetBrowserAsync() => _browserTask;

        public async ValueTask DisposeAsync()
        {
            var browser = await _browserTask;
            if (browser is not null)
                await browser.CloseAsync();
        }
    }
}