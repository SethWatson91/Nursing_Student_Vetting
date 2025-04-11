using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nursing_Student_Vetting.Models;

namespace Nursing_Student_Vetting.Controllers
{
    public class TestController : Controller
    {
        private readonly NursingStudentContext _context;


        public TestController(NursingStudentContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }

        public IActionResult List()
        {
            List<Test> tests = _context.Tests.ToList();
            return View(tests);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return View(new Test());
            }

            Test? tests = _context.Tests.Find(id);
            
            if (tests == null)
            {
                return NotFound();
            }

            return View(tests);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Test testItem)
        {
            if (ModelState.IsValid)
            {
                bool exists = await _context.Tests.AnyAsync(p => p.TestID == testItem.TestID);

                if (!exists)        // if student is new
                {
                    _context.Tests.Add(testItem);
                }
                else
                {
                    _context.Tests.Update(testItem);  // update existing student
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            else
            {
                ViewBag.Student = testItem;

                return View(nameof(List));
            }
        }

        [HttpPost]
        public IActionResult Delete(int? Id)
        {
            try
            {
                var testItem = _context.Tests.FirstOrDefault(p => p.TestID == Id);
                if (testItem == null)
                {
                    return NotFound();
                }
                _context.Tests.Remove(testItem);
                _context.SaveChanges();

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message} - {ex.StackTrace}");
            }
        }
    }
}
