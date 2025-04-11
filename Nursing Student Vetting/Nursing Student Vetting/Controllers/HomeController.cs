using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nursing_Student_Vetting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager; // For role checking
        private readonly NursingStudentContext _context; // For student lookup

        public HomeController(
            ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            NursingStudentContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // Get the current logged-in user
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "Student"))
                {
                    // Fetch the student record linked to this user
                    var student = await _context.Students
                        .FirstOrDefaultAsync(s => s.Email == user.Email); // Adjust based on your model

                    if (student != null)
                    {
                        return RedirectToAction("Details", "Students", new { id = student.StudentID });
                    }
                    else
                    {
                        _logger.LogWarning($"No student record found for email: {user.Email}");
                        return RedirectToAction("Index", "Home"); // Fallback (e.g., homepage)
                    }
                }
                else if (await _userManager.IsInRoleAsync(user, "Admin") || await _userManager.IsInRoleAsync(user, "Teacher"))
                {
                    // Redirect Admin/Teacher to their View
                    return RedirectToAction("List", "Students");
                }
            }

            // Default redirect for authenticated users without specific roles
            return RedirectToAction("Index", "Students");
        }
    }
}