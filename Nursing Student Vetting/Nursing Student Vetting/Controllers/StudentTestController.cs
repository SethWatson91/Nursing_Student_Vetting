using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nursing_Student_Vetting.Models;
using Nursing_Student_Vetting.Helpers;

namespace Nursing_Student_Vetting.Controllers
{
    public class StudentTestController : Controller
    {
        private readonly NursingStudentContext _context;


        public StudentTestController(NursingStudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Update(string studentId, int? testId, int? attemptNumber)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return View(new StudentTest());
            }

            var studentTest = await _context.StudentTests
                .Include(st => st.Student)
                .Include(st => st.Test)
                .FirstOrDefaultAsync(st => st.StudentID == studentId && st.TestID == testId && st.AttemptNumber == attemptNumber);

            if (studentTest == null)
            {
                studentTest = new StudentTest { StudentID = studentId };
            }

            // Ensure these are never null by initializing them
            ViewBag.Tests = await _context.Tests.ToListAsync() ?? new List<Test>();
            return View(studentTest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StudentTest studentTest)
        {
            if (ModelState.IsValid)
            {
                // Check if the StudentClass entry already exists
                var existingStudentTest = await _context.StudentTests
                    .FirstOrDefaultAsync(sc =>
                        sc.StudentID == studentTest.StudentID &&
                        sc.TestID == studentTest.TestID &&
                        sc.AttemptNumber == studentTest.AttemptNumber);

                if (existingStudentTest == null)
                {
                    // If not exists, add it to the database
                    _context.StudentTests.Add(studentTest);
                }
                else
                {
                    // If exists, update it
                    existingStudentTest.Score = studentTest.Score;
                    _context.Update(existingStudentTest);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(StudentsController.Details), 
                    typeof(StudentsController).Name.Replace("Controller", ""),
                    new { id = studentTest.StudentID });
            }


            // If we got this far, something failed; redisplay form
            ViewBag.Tests = await _context.Tests.ToListAsync() ?? new List<Test>();
            return View(studentTest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string studentId, int testId, int attemptNumber)
        {
            var studentTest = await _context.StudentTests.FindAsync(testId, attemptNumber, studentId);
            if (studentTest == null)
            {
                return NotFound();
            }

            _context.StudentTests.Remove(studentTest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Student", new { id = studentId });
        }
    }
}