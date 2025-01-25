using project.Data.DataAccess;
using project.Data.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection.Metadata;
namespace project.Data.Repository;

public class LoginRepository : ILoginRepository
{
    private readonly ISqlDataAccess _db;

    public LoginRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<bool> AddAsync(Users user)
    {
        try
        {

            await _db.SaveData("AddUser", new
            {
                user.Username,
                user.Password,
                user.Email,
                user.PhoneNumber,
                user.Role

            }
                );
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Users?> LoginAsync(string username, string password)
    {
        var results = await _db.GetData<Users, dynamic>("VerifyUser", new
        {
            Username = username,
            Password = password
        });
        var user = results.FirstOrDefault();

        
        return user;
    }
}

  /*  public async Task<bool> AddAsync(Users users)
    {
        try
        {
            await _db.SaveData("AddUser", new { Users.Username, Users.Password, Users.Email,Users.PhoneNumber });
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Users users)
    {
        try
        {
            await _db.SaveData("updateUser", users);
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
            await _db.SaveData("DeleteById", new { Id = id });
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public async Task<Users?> GetByIdAsync(int id)
    {
        IEnumerable<Users> result = await _db.GetData<Users, dynamic>
            ("GetUserById", new { Id = id });
        return result.FirstOrDefault();

    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        string query = "GetAllUsers";
        return await _db.GetData<Users, dynamic>(query, new { });
    }
}
  */

