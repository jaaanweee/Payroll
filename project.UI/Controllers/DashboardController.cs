﻿using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Dash()
        {
            return View();
        }
        public IActionResult Salary()
        {
            return View();
        }
        public IActionResult Leave()
        {
            return View();
        }
        public IActionResult Taxes()
        {
            return View();
        }
        public IActionResult Reimbursements()
        {
            return View();
        }
        public IActionResult Payments()
        {
            return View();
        }
        public IActionResult Alerts()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult PayrollProcess()
        {
            return View();
        }
        public IActionResult TaxReports()
        {
            return View();
        }
        public IActionResult HRReports()
        {
            return View();
        }
        public IActionResult ProfileManagement()
        {
            return View();
        }
        public IActionResult PayrollPolicy()
        {
            return View();
        }
        public IActionResult LeaveManagement()
        {
            return View();
        }
        public IActionResult SystemSettings()
        {
            return View();
        }
        public IActionResult AuditTrail()
        {
            return View();
        }
    }

}
