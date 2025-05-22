
using PuppeteerSharp;

namespace BE_ModernEstate.WebAPI.Configurations.BrowserProvider
{
    public interface IBrowserProvider : IAsyncDisposable
    {
        Task<IBrowser> GetBrowserAsync();
    }
}