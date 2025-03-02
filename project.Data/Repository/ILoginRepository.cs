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


    }
}