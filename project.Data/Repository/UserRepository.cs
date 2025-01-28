using project.Data.DataAccess;
using project.Data.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public UserRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public async Task AddUserAsync(Users user)
        {
            var parameters = new
            {
                Username = user.Username,
                Password = user.Password, // Password should be hashed before saving
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };

            // Assuming you have a stored procedure named AddUser
            await _sqlDataAccess.SaveData("AddUser", parameters);
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            var parameters = new { Id = id };
            // Assuming you have a stored procedure for fetching a user by Id
            var result = await _sqlDataAccess.GetData<Users, dynamic>("GetUserById", parameters);
            return result.FirstOrDefault(); // Get the first or default user
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            // Assuming you have a stored procedure to get all users
            var result = await _sqlDataAccess.GetData<Users, dynamic>("GetAllUsers", new { });
            return result;
        }
        public async Task<Users> GetUserByUsernameAsync(string username)
        {
            var parameters = new { Username = username };
            var result = await _sqlDataAccess.GetData<Users, dynamic>("GetUserByUsername", parameters);
            return result.FirstOrDefault();
        }
        public async Task UpdateUserAsync(Users user)
        {
            var parameters = new
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password, // Hash the password before saving
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };

            // Assuming you have a stored procedure to update user data
            await _sqlDataAccess.SaveData("updateUser", parameters);
        }

        public async Task DeleteUserAsync(int id)
        {
            var parameters = new { Id = id };
            // Assuming you have a stored procedure to delete a user by Id
            await _sqlDataAccess.SaveData("sp_DeleteUser", parameters);
        }
        public async Task<IEnumerable<Users>> SearchUsersAsync(string searchQuery)
        {
            var parameters = new { SearchQuery = searchQuery };

            // You can use a SQL query or stored procedure to search users by ID or Username
            var result = await _sqlDataAccess.GetData<Users, dynamic>("sp_SearchUsers", parameters);
            return result;
        }

        public async Task<IEnumerable<Users>> GetActivatedUsersAsync()
        {
            // Assuming you have a stored procedure to get only activated users
            var result = await _sqlDataAccess.GetData<Users, dynamic>("GetActivatedUsers", new { });
            return result;
        }
        public async Task<IEnumerable<Users>> SearchActivatedUsersAsync(string searchQuery)
        {
            var parameters = new { SearchQuery = searchQuery };

            // Assuming you have a stored procedure or query to search activated users
            var result = await _sqlDataAccess.GetData<Users, dynamic>("sp_SearchActivatedUsers", parameters);
            return result;
        }
        public async Task DeactivateUserAsync(int id)
        {
            var parameters = new { Id = id };
            await _sqlDataAccess.SaveData("sp_DeactivateUser", parameters); // Call stored procedure
        }

        

    }
}
