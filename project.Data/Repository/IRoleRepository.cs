using project.Data.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Role role); // Add role method
        Task<IEnumerable<Role>> GetAllRolesAsync(); // Get all roles method
        Task UpdateRoleAsync(int roleId, Role updatedRole); // Update role method
        Task RemoveRoleAsync(int roleId); // Remove role method
        Task<Role> GetRoleByIdAsync(int id); // Get role by ID method
    }
}
