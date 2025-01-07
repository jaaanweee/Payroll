using project.Data.Models.Domain;
namespace project.Data.Repository
{
    public interface IAllowanceRepository
    {
        Task<bool> AddAsync(Allowance allowance);
        Task<bool> UpdateAsync(Allowance allowance);
        Task<bool> DeleteAsync(int id);

        Task<Allowance?> GetByIdAsync(int id);
        Task<IEnumerable<Allowance>> GetAllAsync();
    }
}