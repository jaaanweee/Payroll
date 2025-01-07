using project.Data.DataAccess;
using project.Data.Models.Domain;
namespace project.Data.Repository;

public class AllowanceRepository : IAllowanceRepository
{
    private readonly ISqlDataAccess _db;

    public AllowanceRepository(ISqlDataAccess db)
    {
        _db = db;
    }
    public async Task<bool> AddAsync(Allowance allowance)
    {
        try
        {
            await _db.SaveData("sp_create_allowance", new { allowance.Allowance_Name, allowance.Description });
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Allowance allowance)
    {
        try
        {
            await _db.SaveData("sp_update_allowance", allowance);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            await _db.SaveData("sp_delete_allowance", new { Id = id });
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
public async Task<Allowance?>GetByIdAsync(int id)
    {
        IEnumerable<Allowance> result = await _db.GetData<Allowance, dynamic>
            ("sp_get_allowance", new { Id = id });
        return result.FirstOrDefault();

    }

public async Task<IEnumerable<Allowance>> GetAllAsync()
    {
        string query = "sp_get_allowances";
        return await _db.GetData<Allowance, dynamic>(query, new { });
    }
}

