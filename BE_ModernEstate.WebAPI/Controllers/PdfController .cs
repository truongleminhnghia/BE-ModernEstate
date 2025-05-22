
using Microsoft.AspNetCore.Mvc;
using ModernEstate.BLL.InvoiceDocuments;
using ModernEstate.DAL.Context;
using QuestPDF.Fluent;

namespace BE_ModernEstate.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/pdf")]
    public class PdfController : ControllerBase
    {

        private readonly ApplicationDbConext _dbContext;
        public PdfController(ApplicationDbConext dbContext)
        {
            _dbContext = dbContext;
        }

        // [HttpGet("download/{id}")]
        // public async Task<IActionResult> DownloadAsync(int id)
        // {
        //     // 1. Kiểm tra database/permission
        //     var invoice = await _dbContext.Invoices
        //         .Where(i => i.Id == id)
        //         .Select(i => new
        //         {
        //             i.Number,
        //             i.Date,
        //             Lines = i.Lines.Select(l => new
        //             {
        //                 l.ProductName,
        //                 l.Quantity,
        //                 l.UnitPrice
        //             }).ToList()
        //         })
        //         .FirstOrDefaultAsync();

        //     if (invoice == null)
        //         return NotFound($"Không tìm thấy hoá đơn với id = {id}");

        //     // 2. Sinh PDF
        //     var doc = new InvoiceDocument(
        //         invoice.Number,
        //         invoice.Date,
        //         invoice.Lines.Select(l => (l.ProductName, l.Quantity, l.UnitPrice)).ToList()
        //     );

        //     byte[] pdfBytes;
        //     using (var ms = new MemoryStream())
        //     {
        //         doc.GeneratePdf(ms);
        //         pdfBytes = ms.ToArray();
        //     }

        //     // 3. Trả về file
        //     var fileName = $"Invoice_{invoice.Number}.pdf";
        //     return File(pdfBytes, "application/pdf", fileName);
        // }

        [HttpGet("demo")]
        public IActionResult Demo()
        {
            // 1. Tạo dữ liệu mẫu
            var sampleLines = new List<(string Item, int Qty, decimal Price)>
        {
            ("Sản phẩm A", 2, 120000m),
            ("Sản phẩm B", 1,  75000m),
            ("Sản phẩm C", 3,  50000m)
        };

            // 2. Sinh PDF với dữ liệu mẫu
            var doc = new InvoiceDocument(
                invoiceNumber: "DEMO-20250522",
                date: DateTime.Now,
                lines: sampleLines
            );

            byte[] pdfBytes;
            using (var ms = new MemoryStream())
            {
                doc.GeneratePdf(ms);
                pdfBytes = ms.ToArray();
            }

            // 3. Trả file PDF về client
            return File(pdfBytes, "application/pdf", "DemoInvoice.pdf");
        }

    }
}