using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project.Data.Models.Domain;
using project.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace project.Controllers
{
    public class PayrollController : Controller
    {
        private readonly IPayrollRepository _context;

        public PayrollController(IPayrollRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> PayrollCalculation()
        {
            ViewData["Title"] = "Payroll Calculation";
            var users = await _context.GetAllUsersForDropdownAsync();

            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(), // Ensure correct ID binding
                Text = u.Username
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PayrollCalculation(PayrollCalculationModel model)
        {
            if (ModelState.IsValid)
            {
                int rowsAffected = await _context.AddPayrollAsync(model);
                if (rowsAffected > 0)
                {
                    TempData["AlertMessage"] = "Payroll data saved successfully!";
                    return RedirectToAction("PayrollCalculation");
                }
            }

            TempData["AlertMessage"] = "Failed to save payroll data!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var payrolls = await _context.GetAllPayrollsAsync(); // Fixed async method
            return View(payrolls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollSlip(int id)
        {
            var payroll = await _context.GetPayrollByIdAsync(id); // Fixed async method
            if (payroll == null)
            {
                return NotFound();
            }

            return View("PayrollSlip", payroll); // Assumes a PayrollSlip.cshtml view exists
        }

        public async Task<IActionResult> DownloadSlip(int id)
        {
            PayrollCalculationModel salarySlip = await _context.GetPayrollByIdAsync(id);
            salarySlip.AmountInWords= NumberToWords((int)salarySlip.NetSalary) + " Rupees Only";
            if (salarySlip == null)
            {
                return NotFound();
            }
            using (var memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Load bold font
                PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                // Add Title with Bold
                document.Add(new Paragraph("Salary Slip").SetFont(boldFont).SetFontSize(16));

                // Add Salary Details
                document.Add(new Paragraph($"User Name: {salarySlip.Username}").SetFont(normalFont));
                document.Add(new Paragraph($"Basic Salary: {salarySlip.BasicSalary:C}").SetFont(normalFont));
                document.Add(new Paragraph($"Allowance: {salarySlip.Allowance:C}").SetFont(normalFont));
                document.Add(new Paragraph($"Deduction: {salarySlip.TotalDeductions:C}").SetFont(normalFont));
                document.Add(new Paragraph($"Net Salary: {salarySlip.NetSalary:C}").SetFont(boldFont)); // Make net salary bold
                document.Add(new Paragraph($"Amount in Words: {salarySlip.AmountInWords}").SetFont(normalFont));
                document.Add(new Paragraph($"Generated On: {salarySlip.CreatedAt:yyyy-MM-dd}").SetFont(normalFont));

                document.Close();

                return File(memoryStream.ToArray(), "application/pdf", "SalarySlip.pdf");
            }

        }

        private string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                          "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

            string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number / 100000 > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }
    }
}
