using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;
using System.Threading.Tasks;

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
        public IActionResult Create()
        {
            return View(); // This will return the view where the admin can add a new user
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                // You might want to hash the password before saving it
                await _userRepository.AddUserAsync(user); // This will call the stored procedure to insert a new user
                TempData["msg"] = "User added successfully!";
                return RedirectToAction("Index"); // Redirect to user list or another page after successful addition
            }
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
    }
}
