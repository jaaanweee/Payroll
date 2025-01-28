using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace project.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: Role/AddRole
        public IActionResult AddRole()
        {
            return View(); // Returns the view to add a new role
        }

        // POST: Role/AddRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleRepository.AddRoleAsync(role); // Add role using repository method
                TempData["msg"] = "Role added successfully!";
                return RedirectToAction("RoleList"); // Redirect to role list after successful addition
            }
            return View(role); // Return view if the model is not valid
        }

        // GET: Role/RoleList
        public async Task<IActionResult> RoleList()
        {
            var roles = await _roleRepository.GetAllRolesAsync(); // Fetch all roles from the database
            return View(roles); // Return the list of roles to the view
        }

        // GET: Role/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> EditR(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id); // Fetch role by its ID
            if (role == null)
            {
                return NotFound(); // Return 404 if the role is not found
            }
            return View(role); // Pass the role to the edit view
        }

        // POST: Role/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(int id, Role updatedRole)
        {
            if (ModelState.IsValid)
            {
                await _roleRepository.UpdateRoleAsync(id, updatedRole); // Update role using repository method
                TempData["msg"] = "Role updated successfully!";
                return RedirectToAction("RoleList"); // Redirect to role list after successful update
            }
            return View(updatedRole); // Return view if the model is not valid
        }

        // POST: Role/RemoveRole/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(int id)
        {
            await _roleRepository.RemoveRoleAsync(id); // Remove role using repository method
            TempData["msg"] = "Role removed successfully!";
            return RedirectToAction("RoleList"); // Redirect to role list after successful removal
        }
    }
}
