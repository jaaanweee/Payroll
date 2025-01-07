using Microsoft.AspNetCore.Mvc;

namespace project.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            // Only accessible by users with "Admin" role
            ViewData["Title"] = "Dashboard Overview";
            return View();
        }

        public IActionResult HRDashboard()
        {
            // Only accessible by users with "HRManager" role
            return View();
        }

        public IActionResult EmployeeDashboard()
        {
            // Only accessible by users with "Employee" role
            return View();
        }
        public IActionResult DashboardOverview()
        {
            ViewData["Title"] = "Dashboard Overview";
            return View();
        }

        public IActionResult UserManagement()
        {
            ViewData["Title"] = "User Management";
            return View();
        }
        public IActionResult PayrollManagement()
        {
            ViewData["Title"] = "PayrollManagement";
            return View();
        }
        public IActionResult EmployeeManagement()
        {
            ViewData["Title"] = "EmployeeManagement";
            return View();
        }

        public IActionResult Reports()
        {
            ViewData["Title"] = "Reports";
            return View();
        }

        public IActionResult Settings()
        {
            ViewData["Title"] = "Settings";
            return View();
        }

        public IActionResult AuditLogs()
        {
            ViewData["Title"] = "AuditLogs";
            return View();
        }

    }

}
