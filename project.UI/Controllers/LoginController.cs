using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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

                // Check if email and phone numbers are consistent between Users and Employees
               // await _usersRepo.CheckEmailPhoneConsistency(userId);

                // Get the employee information based on the UserID
                UserLoginModel employee = await _usersRepo.GetEmployeeInfo(userId);

                if (employee != null)
                {
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
    }
}
    
       /* public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Find user by username or email
            var user = await _usersRepo.GetUserByUsernameOrEmailAsync(model.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View(model);
            }

            // Generate a verification code
            var verificationCode = new Random().Next(100000, 999999).ToString();
            TempData["ForgotPasswordVerificationCode"] = verificationCode;
            TempData["ForgotPasswordUsername"] = user.Username;

            // Send verification code via email or SMS
            if (model.SendByEmail)
            {
                await _emailService.SendVerificationCodeAsync(user.Email, verificationCode);
            }
            else
            {
                await _smsService.SendVerificationCodeAsync(user.PhoneNumber, verificationCode);
            }

            return RedirectToAction("VerifyForgotPasswordCode");
        }

        public IActionResult VerifyForgotPasswordCode()
        {
            return View(new VerifyCodeViewModel());
        }

        [HttpPost]
        public IActionResult VerifyForgotPasswordCode(VerifyCodeViewModel model)
        {
            var verificationCode = TempData["ForgotPasswordVerificationCode"] as string;
            var username = TempData["ForgotPasswordUsername"] as string;

            if (model.Code == verificationCode)
            {
                TempData.Remove("ForgotPasswordVerificationCode");
                TempData.Remove("ForgotPasswordUsername");
                return RedirectToAction("ResetPassword", new { username });
            }

            ModelState.AddModelError("", "Invalid verification code.");
            return View(model);
        }

        public IActionResult ResetPassword(string username)
        {
            var model = new ResetPasswordViewModel { Username = username };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _usersRepo.GetUserByUsernameAsync(model.Username);
            if (user != null)
            {
                user.Password = model.NewPassword; // Hash the password in production
                await _usersRepo.UpdateUserAsync(user);
                TempData["msg"] = "Password has been reset successfully.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "User not found.");
            return View(model);
        }

    }

}
*/
       


/*
public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Users users)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(users);
                bool addUsersResult = await _usersRepo.AddAsync(users);
                if (addUsersResult)
                    TempData["msg"] = "user added";
                else
                    TempData["msg"] = "could not be added";

            }
            catch (Exception ex)
            {
                TempData["msg"] = "could not be added";
            }
            return RedirectToAction(nameof(Add));

        }

        public async Task<IActionResult> Edit(int id)
        {
            var users = await _usersRepo.GetByIdAsync(id);
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Users users)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(users);
                var updateUsersResult = await _usersRepo.UpdateAsync(users);
                if (updateUsersResult)
                    TempData["msg"] = "successfully updated";
                else
                    TempData["msg"] = "could not be updtaed";


            }
            catch (Exception ex)
            {
                TempData["msg"] = "could not be updtaed";

            }
            return View(users);

        }

        // public async Task<IActionResult> GetById(int id)
        //{
        //  return View();
        //}
        public async Task<IActionResult> DisplayAll()
        {
            var userss = await _usersRepo.GetAllAsync();
            return View(userss);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _usersRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAll));
        }
    }
}
*/