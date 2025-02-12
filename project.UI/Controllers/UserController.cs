using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Globalization;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: User/Create
        public async Task<IActionResult> Create()
        {
            var model = new Users(); // Creates a new instance to avoid null issues
            return View(model);

        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user)
        {
         
                // You might want to hash the password before saving it
                await _userRepository.AddUserAsync(user); // This will call the stored procedure to insert a new user
                TempData["msg"] = "User added successfully!";
                return RedirectToAction("Index"); // Redirect to user list or another page after successful addition
            
            return View(user);
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync(); // Fetch all users
            return View(users); // Return the Index view with the list of users
        }
        public async Task<IActionResult> UserManagement()
        {
            ViewData["Title"] = "User Management";
            var users = await _userRepository.GetAllUsersAsync(); // Fetch users from the database
            return View(users); // Pass the user list to the view
        }
        // Action to get the EditUserPage with search functionality
        [HttpGet]
        public async Task<IActionResult> EditUserPage(string searchQuery)
        {
            IEnumerable<Users> users;
            if (string.IsNullOrEmpty(searchQuery))
            {
                users = await _userRepository.GetAllUsersAsync(); // Get all users
            }
            else
            {
                // Perform the search using the stored procedure
                users = await _userRepository.SearchUsersAsync(searchQuery);
            }
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Save edited user details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Users user)
        {
          
                // Update the user in the database
                await _userRepository.UpdateUserAsync(user);

                // Redirect to the list of users or a success page
                return RedirectToAction("EditUserPage");
            

            // If validation fails, return to the Edit view with the user's data
            return View("Edit", user);
        }

        // Action for deactivating users (only showing active users)
        [HttpGet]
        public async Task<IActionResult> DeactivateUserPage(string searchQuery)
        {
            IEnumerable<Users> users;

            if (string.IsNullOrEmpty(searchQuery))
            {
                // Get all activated users (IsActive = true)
                users = await _userRepository.GetActivatedUsersAsync();
            }
            else
            {
                // Perform the search using the stored procedure or method, but only for activated users
                users = await _userRepository.SearchActivatedUsersAsync(searchQuery);
            }

            return View(users); // Pass the activated users to the DeactivateUserPage view
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _userRepository.DeactivateUserAsync(id); // Call repository method
            TempData["msg"] = "User deactivated successfully!";
            return RedirectToAction("DeactivateUserPage"); // Redirect to DeactivateUserPage
        }

        // Deleting a user
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return RedirectToAction("UserManagement"); // Redirect back to User Management page
        }


        [HttpGet]
        public async Task<IActionResult>  Processing()
        {
            var users = await _userRepository.GetAllUsersAsync(); // Fetch all users
            return View(users); // Return the Index view with the list of users
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Processing(SalarySlip salarySlip)
        {
            if (ModelState.IsValid)
            {
                // Calculate net salary
                salarySlip.NetSalary = salarySlip.BasicSalary + salarySlip.Allowance - salarySlip.Deduction;

                // Convert salary amount to words
                salarySlip.AmountInWords = ConvertToWords((int)salarySlip.NetSalary);

                // Save salary details to database
                await _userRepository.SaveSalarySlipAsync(salarySlip);

                TempData["msg"] = "Salary slip generated successfully!";
                return RedirectToAction("SalarySlipDetails", new { id = salarySlip.UserId });
            }
            return View(salarySlip);
        }

        [HttpGet]
        public async Task<IActionResult> SalarySlipDetails(int id)
        {
            var salarySlip = await _userRepository.GetSalarySlipByEmployeeIdAsync(id);
            if (salarySlip == null)
            {
                return NotFound();
            }
            return View(salarySlip);
        }

        private string ConvertToWords(int num)
        {
            if (num == 0) return "Zero";

            string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (num < 20) return ones[num];
            if (num < 100) return tens[num / 10] + (num % 10 > 0 ? " " + ones[num % 10] : "");
            if (num < 1000) return ones[num / 100] + " Hundred" + (num % 100 > 0 ? " and " + ConvertToWords(num % 100) : "");
            if (num < 1000000) return ConvertToWords(num / 1000) + " Thousand" + (num % 1000 > 0 ? " " + ConvertToWords(num % 1000) : "");
            return ConvertToWords(num / 1000000) + " Million" + (num % 1000000 > 0 ? " " + ConvertToWords(num % 1000000) : "");
        }



        //public async Task<IActionResult> Getslip(int id)
        //{
        //    var user = await _userRepository.GetUserByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user); // Pass user data to view
        //}


        public async Task<IActionResult> Getslip(int id)
        {
            var salarySlip = await _userRepository.GetSalarySlipByIdAsync(id);

            if (salarySlip == null)
            {
                return NotFound();
            }

            return View(salarySlip); // Pass salary slip data to view
        }

        public async Task<IActionResult> DownloadSlip(int id)
        {
            var salarySlip = await _userRepository.GetSalarySlipByIdAsync(id);

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
                document.Add(new Paragraph($"Employee Name: {salarySlip.Name}").SetFont(normalFont));
                document.Add(new Paragraph($"Designation: {salarySlip.Designation}").SetFont(normalFont));
                document.Add(new Paragraph($"Joining Date: {salarySlip.JoiningDate:yyyy-MM-dd}").SetFont(normalFont));
                document.Add(new Paragraph($"Basic Salary: {salarySlip.BasicSalary:C}").SetFont(normalFont));
                document.Add(new Paragraph($"Allowance: {salarySlip.Allowance:C}").SetFont(normalFont));
                document.Add(new Paragraph($"Deduction: {salarySlip.Deduction:C}").SetFont(normalFont));
                document.Add(new Paragraph($"Net Salary: {salarySlip.NetSalary:C}").SetFont(boldFont)); // Make net salary bold
                document.Add(new Paragraph($"Amount in Words: {salarySlip.AmountInWords}").SetFont(normalFont));
                document.Add(new Paragraph($"Generated On: {salarySlip.CreatedAt:yyyy-MM-dd}").SetFont(normalFont));

                document.Close();

                return File(memoryStream.ToArray(), "application/pdf", "SalarySlip.pdf");
            }

        }


    }
}

    
 
