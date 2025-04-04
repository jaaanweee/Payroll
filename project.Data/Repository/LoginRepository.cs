using project.Data.DataAccess;
using project.Data.Models.Domain;
namespace project.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ISqlDataAccess _db;

        // Constructor that injects ISqlDataAccess
        public LoginRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        // Add new user to the database
        public async Task<bool> AddAsync(Users user)
        {
            try
            {
                // Use SaveData method to call stored procedure
                await _db.SaveData("AddUser", new
                {
                    user.Username,
                    user.Password,
                    user.Email,
                    user.PhoneNumber,
                    user.Role
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Verify user credentials during login
        public async Task<Users?> LoginAsync(string username, string password)
        {
            // Use GetData method to call stored procedure and retrieve user info
            var results = await _db.GetData<Users, dynamic>("VerifyUser", new
            {
                Username = username,
                Password = password
            });

            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<UserLoginHistoryViewModel>> GetUserLoginHistoryAsync()
        {
            var result = await _db.GetData<UserLoginHistoryViewModel, dynamic>("sp_GetUserLoginHistory", new { });
            return result;
        }

        public async Task LogUserLoginAsync(int userId)
        {
            await _db.SaveData("LogUserLogin", new { UserId = userId });
        }
        public async Task LogUserLogoutAsync(int userId)
        {
            await _db.SaveData("LogUserLogout", new
            {
                UserID = userId
            });
        }



        // Check if email and phone numbers are consistent between Users and Employees
        public async Task CheckEmailPhoneConsistency(int userId)
        {
            // Use SaveData to call the CheckEmailPhoneConsistency stored procedure
            await _db.SaveData("CheckEmailPhoneConsistency", new
            {
                UserID = userId
            });
        }

        // Get employee details based on UserID
        public async Task<UserLoginModel?> GetEmployeeInfo(int userId)
        {
            // Use GetData to call the GetEmployeeInfo stored procedure and retrieve employee data
            var employees = await _db.GetData<UserLoginModel, dynamic>("GetEmployeeInfo", new
            {
                UserID = userId
            });

            return employees.FirstOrDefault();
        }

        public async Task<Users?> GetUserByPhoneAsync(string phoneNumber)
        {
            var result = await _db.GetData<Users, dynamic>("GetUserByPhone", new { PhoneNumber = phoneNumber });
            return result.FirstOrDefault();
        }

        public async Task UpdatePasswordAsync(int userId, string newPassword)
        {
            await _db.SaveData("sp_UpdatePassword", new { UserId = userId, NewPassword = newPassword });
        }

        public async Task SaveOtpAsync(string email, string otp)
        {
            await _db.SaveData("sp_SaveOtp", new { Email = email, Otp = otp });
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            var result = await _db.GetData<Users, dynamic>("sp_GetUserByEmail", new { Email = email });
            return result.FirstOrDefault();
        }
        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var result = await _db.GetData<int, dynamic>("sp_ValidateOtp", new { Email = email, Otp = otp });
            return result.FirstOrDefault() == 1; // Returns true if OTP matches
        }


    }
}




