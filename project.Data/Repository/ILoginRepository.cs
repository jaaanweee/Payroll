using project.Data.Models.Domain;
namespace project.Data.Repository
{
    public interface ILoginRepository
    {
        Task<Users?> LoginAsync(string username, string password);
        Task<bool> AddAsync(Users user);
    }
}