using project.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(Users user);              // Renamed to AddUserAsync for async pattern
        Task<Users> GetUserByIdAsync(int? id);        // Renamed to include Async and added `Async` suffix
        Task<IEnumerable<Users>> GetAllUsersAsync();
        Task<IEnumerable<Users>> GetAUsersAsync();
        Task<IEnumerable<Salary>> GetAllSalariesAsync();
        // Renamed to GetAllUsersAsync for async pattern
        Task<Users> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(Users user);            // Renamed to UpdateUserAsync for async pattern
        Task DeleteUserAsync(int id);   // Renamed to DeleteUserAsync for async pattern
        
        // New method for searching users
        Task<IEnumerable<Users>> SearchUsersAsync(string searchQuery);
        Task<IEnumerable<Users>> GetActivatedUsersAsync();
        Task<IEnumerable<Users>> SearchActivatedUsersAsync(string searchQuery);

        Task DeactivateUserAsync(int id);
        Task EmpUpdProfileAsync(Users user1);



        Task SaveSalarySlipAsync(SalarySlip salarySlip); 
        Task<SalarySlip> GetSalarySlipByEmployeeIdAsync(int employeeId);
        Task<SalarySlip> GetSalarySlipByIdAsync(int id);
        Task<IEnumerable<Users>> GetEmployeesAsync();
        Task<string> UpdatePasswordAsync(int userId, string currentPassword, string newPassword);


    }
}
