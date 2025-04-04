using project.Data.Models.Domain;
namespace project.Data.Repository
{
    public interface ILoginRepository
    {
        Task<Users?> LoginAsync(string username, string password);
        Task<bool> AddAsync(Users user);
        Task CheckEmailPhoneConsistency(int userId);
        Task<UserLoginModel> GetEmployeeInfo(int userId);
        Task LogUserLoginAsync(int userId);
        Task LogUserLogoutAsync(int userId);
        Task<IEnumerable<UserLoginHistoryViewModel>> GetUserLoginHistoryAsync();
        Task<Users?> GetUserByPhoneAsync(string phoneNumber);
        Task UpdatePasswordAsync(int userId, string newPassword);
        Task SaveOtpAsync(string email, string otp);
        Task<Users?> GetUserByEmailAsync(string email);
        Task<bool> ValidateOtpAsync(string email, string otp);
    }
}