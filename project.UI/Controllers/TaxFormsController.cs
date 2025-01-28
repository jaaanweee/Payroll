using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace project.UI.Controllers
{
    public class TaxFormsController : Controller
    {
        private readonly IConfiguration _configuration;

        public TaxFormsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult DownloadTaxForm(string formName)
        {
            var pdfPath = _configuration["appSettings:PdfReportPath"];

            // Combine the path with the formName
            //var filePath = Path.Combine(pdfPath, formName);
            string filePath = "D:\\College-Project\\Payroll\\project.Data\\PDF\\";


            // Check if the file exists
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/pdf", formName);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
