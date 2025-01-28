using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace project.UI.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IActionResult Expense()
        {
            return View(new Expense()); // Load the form
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Expense(Expense expense, IFormFile receiptFile)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(expense);
            }

            // Define the folder path inside wwwroot
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "receipts");

            // Check if the folder exists, if not, create it
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Handle file upload
            if (receiptFile != null && receiptFile.Length > 0)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{receiptFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await receiptFile.CopyToAsync(stream);
                }

                expense.ReceiptPath = "/receipts/" + uniqueFileName; // Save relative path
            }

            await _expenseRepository.AddExpenseAsync(expense);
            TempData["AlertMessage"] = "Expense claim submitted successfully!";
            return RedirectToAction("Expense");
        }
    }
}
