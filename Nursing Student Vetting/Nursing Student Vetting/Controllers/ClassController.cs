using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nursing_Student_Vetting.Models;

namespace Nursing_Student_Vetting.Controllers
{
    public class ClassController : Controller
    {
        private readonly NursingStudentContext _context;
        private List<Class> classes;

        public ClassController(NursingStudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List()
        {
            List<Class> classes = _context.Classes.ToList(); // List classes for view
            return View(classes);
        }

        [HttpGet]
        public IActionResult Update(int? classId, string id)
        {
            if (classId == null || id == null)
            {
                return View(new Class()); // new class
            }

            Class? classes = _context.Classes.Find(classId, id); // find based on composite key

            if (classes == null)
            {
                return NotFound();
            }

            return View(classes);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Class classItem)
        {
            if (ModelState.IsValid)
            {
                bool exists = await _context.Classes.AnyAsync(p => p.ClassID == classItem.ClassID && p.CategoryPrefix == classItem.CategoryPrefix);

                if (!exists)        // if student is new
                {
                    _context.Classes.Add(classItem);
                }
                else
                {
                    _context.Classes.Update(classItem);  // update existing student
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            else
            {
                ViewBag.Student = classItem;

                return View(nameof(List));
            }
        }

        [HttpPost] // basic delete action
        public async Task<IActionResult> Delete(string id, int classId)
        {
             Class? studentClass = await _context.Classes
                .FirstOrDefaultAsync(sc => sc.ClassID == classId && sc.CategoryPrefix == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(studentClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ClassController.List),
                typeof(ClassController).Name.Replace("Controller", ""),
                new { id = classId });
            
        }
    }
}
