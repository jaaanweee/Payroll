using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;

namespace project.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _usersRepo;
        public LoginController(ILoginRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }

        public IActionResult Login()
        {

            return View(new UserLoginModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            // Validate the username and password using the repository
            var user = await _usersRepo.LoginAsync(loginModel.Username, loginModel.Password);

            if (user != null && user.Role == loginModel.Role)
            {
                // Get the UserID based on the username
                int userId = user.Id;
                await _usersRepo.LogUserLoginAsync(userId);
                UserLoginModel user1 = await _usersRepo.GetEmployeeInfo(userId);

                if (user1 != null)
                {
                    HttpContext.Session.SetString("Username", user1.Username);
                    HttpContext.Session.SetInt32("UserId", user1.Id);

                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("DashboardOverview", "Dashboard");
                    }
                    else if (user.Role == "HRManager")
                    {
                        return RedirectToAction("HRDashboard", "Dashboard");
                    }
                    else if (user.Role == "Employee")
                    {
                        return RedirectToAction("Dash", "Dashboard");
                    }
                }
            }

            TempData["msg"] = "Invalid credentials, please try again.";
            return View(new UserLoginModel());
        }



        // Display Sign Up View
        public IActionResult SignUp()
        {

            return View(new UserRegistrationModel());
        }

        // Handle Sign Up POST
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationModel);
            }


            bool addUserResult = await _usersRepo.AddAsync(new Users
            {
                Username = registrationModel.Username,
                Password = registrationModel.Password,  // In production, ensure to hash passwords
                Email = registrationModel.Email,
                PhoneNumber = registrationModel.PhoneNumber,
                Role = registrationModel.Role
            });

            TempData["msg"] = addUserResult ? "Sign up successful!" : "Sign up failed.";
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Logout()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                _usersRepo.LogUserLogoutAsync(userId.Value).Wait(); // Log logout time
            }
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Login", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> UserLoginHistory()
        {
            var history = await _usersRepo.GetUserLoginHistoryAsync();
            return View(history);
        }

    }
}




