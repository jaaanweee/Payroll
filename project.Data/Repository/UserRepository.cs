using project.Data.DataAccess;
using project.Data.Models.Domain;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using BCrypt.Net;

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

        public async Task<Users> GetUserByIdAsync(int? id)
        {
            var parameters = new { Id = id };
            // Assuming you have a stored procedure for fetching a user by Id
            var result = await _sqlDataAccess.GetData<Users, dynamic>("GetUserById", parameters);
            return result.FirstOrDefault(); // Get the first or default user
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            // Assuming you have a stored procedure to get all users
            var result = await _sqlDataAccess.GetData<Users, dynamic>("GetAllUser", new { });
            return result;
        }
        public async Task<IEnumerable<Users>> GetAUsersAsync()
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
       public async Task EmpUpdProfileAsync(Users user1)
        {
            var parameters = new
            {
                Id = user1.Id,
                FirstName = user1.FirstName,
                Email = user1.Email,
                PhoneNumber = user1.PhoneNumber,
                Address = user1.Address,
              
            };

            // Assuming you have a stored procedure named AddUser
            await _sqlDataAccess.SaveData("UpdateEP", parameters);
        }

        public async Task SaveSalarySlipAsync(SalarySlip salarySlip)
        {
            var parameters = new
            {
                Userid = salarySlip.UserId,
                Name = salarySlip.Name,
                Designation = salarySlip.Designation,
                JoiningDate = salarySlip.JoiningDate,
                BasicSalary = salarySlip.BasicSalary,
                Allowance = salarySlip.Allowance,
                Deduction = salarySlip.Deduction,
                NetSalary = salarySlip.NetSalary,
                AmountInWords = salarySlip.AmountInWords
            };

            await _sqlDataAccess.SaveData("sp_SaveSalarySlip", parameters);
        }

        public async Task<SalarySlip> GetSalarySlipByEmployeeIdAsync(int employeeId)
        {
            var parameters = new { EmployeeId = employeeId };
            var result = await _sqlDataAccess.GetData<SalarySlip, dynamic>("sp_GetSalarySlipByEmployeeId", parameters);
            return result.FirstOrDefault();
        }

        public async Task<SalarySlip> GetSalarySlipByIdAsync(int id)
        {
            var parameters = new { Id = id };

            // Assuming you have a stored procedure named "GetSalarySlipById"
            var result = await _sqlDataAccess.GetData<SalarySlip, dynamic>("sp_GetSalarySlipById", parameters);

            return result.FirstOrDefault(); // Return the first matching salary slip or null
        }

        public async Task<IEnumerable<Users>> GetEmployeesAsync()
        {
            var result = await _sqlDataAccess.GetData<Users, dynamic>(
                "sp_GetAllEmployees", new { });

            return result;
        }

        public async Task<IEnumerable<Salary>> GetAllSalariesAsync()
        {
            var result = await _sqlDataAccess.GetData<Salary, dynamic>("sp_GetAllSalaries", new { });
            return result;
        }
        public async Task<string> UpdatePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return "User not found.";

            // ✅ Check if the current password matches the hashed password in DB
            if ((currentPassword!=user.Password))
            {
                return "Current password is incorrect.";
            }
            if(newPassword == user.Password)
            {
                return "New password should not be the same as the old password.";
            }

            
            var parameters = new
            {
                UserId = userId,
                NewPassword = newPassword 
            };

            await _sqlDataAccess.SaveData("sp_UpdatePassword", parameters);

            return "Success";
        }

       

    }
}
