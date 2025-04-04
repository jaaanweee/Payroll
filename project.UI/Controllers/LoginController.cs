using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;
using project.Data.Services;
using IEmailService = project.Data.Repository.IEmailService;

namespace project.UI.Controllers
{
    public class LoginController : Controller
    {       
        private readonly ILoginRepository _usersRepo;
        private readonly IEmailService _emailService;

        public LoginController(ILoginRepository usersRepo, IEmailService emailService)
        {
            _usersRepo = usersRepo;
            _emailService = emailService;
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _usersRepo.GetUserByEmailAsync(email);
            if (user == null)
            {
                TempData["msg"] = "Email not found!";
                return View();
            }

            // Generate OTP
            string otp = new Random().Next(100000, 999999).ToString();

            // Save OTP to database (or cache)
            await _usersRepo.SaveOtpAsync(user.Email, otp);

            // Send OTP via email (implement email service)
            await _emailService.SendEmailAsync(user.Email, "Password Reset OTP", $"Your OTP is {otp}");

            TempData["msg"] = "OTP sent to your email.";
            return RedirectToAction("VerifyOtp", new { email });
        }

        [HttpGet]
        public IActionResult VerifyOtp(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyOtp(string email, string otp)
        {
            var user = await _usersRepo.GetUserByEmailAsync(email);
            if (user == null || !await _usersRepo.ValidateOtpAsync(user.Email, otp))
            {
                TempData["msg"] = "Invalid OTP!";
                return View();
            }

            // Redirect to reset password page
            return RedirectToAction("ResetPassword", new { email });
        }

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string newPassword)
        {
            var user = await _usersRepo.GetUserByEmailAsync(email);
            if (user == null)
            {
                TempData["msg"] = "User not found!";
                return View();
            }

            await _usersRepo.UpdatePasswordAsync(user.Id, newPassword);
            TempData["msg"] = "Password reset successful!";
            return RedirectToAction("Login");
        }


    }
}




