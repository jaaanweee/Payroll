using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;

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
        public IActionResult PayrollCalculation()
        {
            ViewData["Title"] = "Payroll Calculation";
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

    }
}
