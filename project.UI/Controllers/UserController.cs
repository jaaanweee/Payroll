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
            if (ModelState.IsValid)
            {
                // Update the user in the database
                await _userRepository.UpdateUserAsync(user);

                // Redirect to the list of users or a success page
                return RedirectToAction("EditUserPage");
            }

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
       
    }
}

    
 
