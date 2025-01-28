using project.Data.DataAccess;
using project.Data.Models.Domain;
using System.Linq;
using System.Threading.Tasks;

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

