using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult>  PayrollCalculation()
        {
            ViewData["Title"] = "Payroll Calculation";
            var users = await _context.GetAllUsersForDropdownAsync(); // Fetch users from repository
            //ViewBag.Users = new SelectList(users, "Id", "Username"); // Populate dropdown
            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.Username, // 🔹 Ensure this is Username
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

    }
}
