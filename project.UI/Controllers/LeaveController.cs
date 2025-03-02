using Microsoft.AspNetCore.Mvc;
using project.Data.Models.Domain;
using project.Data.Repository;

namespace project.UI.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeaveController(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }
        public IActionResult Leave()
        {
            var leave = new Leaves(); // Ensure model is initialized
            return View(leave); // Pass the model to the viewreturn View(); // This will return the view where the admin can add a new user
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leave(Leaves leave)
        {
            // Access the UserID from the session
            int? userId = HttpContext.Session.GetInt32("UserId");

            // If the UserId is not found, redirect to the login page
            if (userId == null)
            {
                TempData["ErrorMessage"] = "You need to be logged in to submit a leave request.";
                return RedirectToAction("Login", "Account"); // Adjust to your login route
            }

            // Set the UserID for the leave object
            leave.UserID = userId.Value;

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Or log the error messages
                }
            }

            if (ModelState.IsValid)
            {

                // Handle the case where this is a new leave request
                await _leaveRepository.AddLeaveAsync(leave);
                TempData["AlertMessage"] = "Leave request submitted successfully!";


            }
            return View(leave);
        }

        public async Task<IActionResult> Index()
        {
            var leaves = await _leaveRepository.GetAllLeavesAsync();
            return View(leaves);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveLeave(int id)
        {
            await _leaveRepository.UpdateLeaveStatusAsync(id, 2); // 2 = Approved
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RejectLeave(int id)
        {
            await _leaveRepository.UpdateLeaveStatusAsync(id, 3); // 3 = Rejected
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetLeaveById()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["ErrorMessage"] = "You need to be logged in to view your leaves.";
                return RedirectToAction("Login", "Account");
            }

            var leaves = await _leaveRepository.GetLeavesByUserIdAsync(userId.Value);
            return View(leaves);
        }

    }
}
