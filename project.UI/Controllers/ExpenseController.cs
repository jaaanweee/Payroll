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
        {// Access the UserID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // If the UserId is not found, redirect to the login page
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You need to be logged in to submit a expense request.";
                return RedirectToAction("Login", "Account"); // Adjust to your login route
            }

            // Set the UserID for the leave object
            expense.UserID = userId.Value;
            ModelState.Remove("ReceiptFileName");
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(expense);
            }

            // Check if a file is uploaded
            if (receiptFile.FileName != null )
            {
                // Define the folder path inside wwwroot
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "receipts");

                // Check if the folder exists, if not, create it
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique file name to avoid conflicts
                var uniqueFileName = $"{Guid.NewGuid()}_{receiptFile.FileName}";

                // Combine folder path and file name
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await receiptFile.CopyToAsync(stream);
                }

                // Save the file name to the model
                expense.ReceiptFileName = uniqueFileName;
            }

            // Save expense details
            await _expenseRepository.AddExpenseAsync(expense);

            TempData["AlertMessage"] = "Expense claim submitted successfully!";
            return RedirectToAction("Expense");
        }

    }
}
