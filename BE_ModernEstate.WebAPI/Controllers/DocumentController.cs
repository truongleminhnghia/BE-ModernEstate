using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/documents")]
    public class DocumentController : ControllerBase
    {
        [HttpGet("modern-estate")]
        public async Task<IActionResult> ModernEstatePdf()
        {
            // 1. HTML mẫu với CSS inline
            var html = @"
                <!DOCTYPE html>
                <html lang=""vi"">
                <head>
                  <meta charset=""UTF-8"">
                  <title>Modern Estate</title>
                  <style>
                    body {
                      margin: 0;
                      padding: 0;
                      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                      background-color: #f0f2f5;
                      display: flex;
                      justify-content: center;
                      align-items: center;
                      height: 100vh;
                    }
                    .card {
                      background: white;
                      border-radius: 12px;
                      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
                      padding: 40px;
                      text-align: center;
                      max-width: 600px;
                    }
                    .card h1 {
                      margin: 0 0 20px;
                      font-size: 32px;
                      color: #2c3e50;
                    }
                    .card p {
                      margin: 0;
                      font-size: 18px;
                      color: #34495e;
                    }
                  </style>
                </head>
                <body>
                  <div class=""card"">
                    <h1>Chào mừng đến với Modern Estate</h1>
                    <p>Hãy khám phá ngôi nhà mơ ước của bạn cùng chúng tôi!</p>
                  </div>
                </body>
                </html>
                ";

            // 2. Tải Chromium
            var browserFetcher = new BrowserFetcher();
            var revisionInfo = await browserFetcher.DownloadAsync();  // dùng bản mặc định

            // 3. Khởi Chromium headless
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                ExecutablePath = revisionInfo.GetExecutablePath(),
                Headless = true
            });

            // 4. Mở trang và đổ nội dung HTML
            using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html, new NavigationOptions
            {
                WaitUntil = new[] { WaitUntilNavigation.Networkidle0 }
            });

            // 5. Xuất PDF (với nền background, margin 20px)
            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions { Top = "20px", Bottom = "20px", Left = "20px", Right = "20px" }
            });

            // 6. Trả file về client
            return File(pdfBytes, "application/pdf", "ModernEstate_Welcome.pdf");
        }
    }
}