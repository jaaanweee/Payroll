using project.Data.DataAccess;
using project.Data.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public RoleRepository(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        // Add a new role to the database
        public async Task AddRoleAsync(Role role)
        {
            var parameters = new { RoleName = role.RoleName }; // Assuming Role has a 'Name' property
            await _sqlDataAccess.SaveData("sp_AddRole", parameters); // Stored procedure to add a role
        }

        // Get all roles from the database
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var result = await _sqlDataAccess.GetData<Role, dynamic>("sp_GetAllRoles", new { });
            return result;
        }

        // Update an existing role by its ID
        public async Task UpdateRoleAsync(int roleId, Role updatedRole)
        {
            var parameters = new { RoleId = roleId, RoleName = updatedRole.RoleName };
            await _sqlDataAccess.SaveData("sp_UpdateRole", parameters); // Stored procedure to update role
        }

        // Remove a role from the database
        public async Task RemoveRoleAsync(int roleId)
        {
            var parameters = new { RoleId = roleId };
            await _sqlDataAccess.SaveData("sp_DeleteRole", parameters); // Stored procedure to delete role
        }

        // Get a role by its ID
        public async Task<Role> GetRoleByIdAsync(int id)
        {
            var result = await _sqlDataAccess.GetData<Role, dynamic>("sp_GetRoleById", new { RoleId = id });
            return result.FirstOrDefault(); // Assuming stored procedure returns a single role
        }
    }
}
