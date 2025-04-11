using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nursing_Student_Vetting.Helpers;
using Nursing_Student_Vetting.Models;

namespace Nursing_Student_Vetting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly NursingStudentContext _context;
        private readonly RequirementsHelper _requirementsHelper;
        private readonly GradeHelper _gradeHelper;
        private readonly EvaluationHelper _evaluationHelper;
        private List<Student> students;


        public StudentsController(NursingStudentContext context, RequirementsHelper requirementsHelper, GradeHelper gradeHelper, EvaluationHelper evaluationHelper)
        {
            _context = context;
            _requirementsHelper = requirementsHelper;
            _gradeHelper = gradeHelper;
            _evaluationHelper = evaluationHelper;
        }


        public IActionResult Index()
        {
            return RedirectToAction(nameof(List));
        }



        [Authorize(Roles = "Admin,Teacher")]
        // GET: Students/List
        public async Task<IActionResult> List(string sortOrder, string searchString)
        {
            ViewData["CurrentSort"] = sortOrder;        //Set in the Switch statement Default is LastName > FirstName 
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["IdSortParm"] = sortOrder == "Id" ? "id_desc" : "Id";
            ViewData["EvalSortParm"] = sortOrder == "evaluationscore" ? "eval_ascen" : "evaluationscore";
            ViewData["CurrentFilter"] = searchString;

            var students = from i in _context.Students
                           select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString) ||
                s.FirstName.Contains(searchString) || s.StudentID.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "evaluationscore":
                    students = students.OrderByDescending(e => e.EvaluationScore);
                    break;

                case "eval_ascen":
                    students = students.OrderBy(e => e.EvaluationScore);
                    break;

                case "name_desc":
                    students = students.OrderByDescending(i => i.LastName).ThenBy(i => i.FirstName);
                    break;
                case "Id":
                    students = students.OrderBy(i => i.StudentID);
                    break;
                case "id_desc":
                    students = students.OrderByDescending(i => i.StudentID);
                    break;
                default:
                    students = students.OrderBy(i => i.LastName).ThenBy(i => i.FirstName);
                    break;
            }

            var studentList = await students.AsNoTracking().ToListAsync();

            // Update each student's EvaluationScore
            foreach (var student in studentList)
            {
                int evaluationScore = await _evaluationHelper.CalculateEvaluationScore(student.StudentID);

                if (student.EvaluationScore == evaluationScore)
                {
                    break;
                }
                else
                {
                    student.EvaluationScore = evaluationScore;

                    _context.Students.Update(student);

                    try { _context.SaveChanges(); }
                    catch { throw new Exception(); }
                }
            }
            return View(studentList);
        }


        [HttpGet]
        public IActionResult Update(string? id)
        {
            if (id == null)
            {
                return View(new Student()); // New student
            }

            Student? student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Student student)
        {

            if (ModelState.IsValid)
            {
                bool exists = await _context.Students.AnyAsync(p => p.StudentID == student.StudentID);

                if (!exists)        // if student is new
                {
                    _context.Students.Add(student);
                }
                else
                {
                    _context.Students.Update(student);  // update existing student
                }
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.Student = students;

                return View("List");
            }
        }


        [HttpPost] // basic delete action
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var student = _context.Students.FirstOrDefault(p => p.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Student? student = await _context.Students
                .Include(s => s.StudentClasses).ThenInclude(sc => sc.Class)
                .Include(s => s.StudentTests).ThenInclude(st => st.Test)
                .FirstOrDefaultAsync(m => m.StudentID == id);

            if (student == null)
            {
                return NotFound();
            }

            //Restrict students to their own details
            if (User.IsInRole("Student"))
            {
                var userEmail = User.Identity.Name; // Assuming email is the username
                if (student.Email != userEmail)
                {
                    return Forbid(); // Or redirect to an access-denied page
                }
            }

            int evaluationScore = await _evaluationHelper.CalculateEvaluationScore(id);
            ViewBag.EvaluationScore = evaluationScore;

            decimal GPA = Math.Round(await _gradeHelper.GetGPA(id), 2);
            ViewBag.GPA = GPA;

            bool humanitiesComplete = _requirementsHelper.IsHumanitiesComplete(id);
            ViewBag.HumanitiesComplete = humanitiesComplete;

            bool requirementsComplete = _requirementsHelper.AreRequirementsMet(id);
            ViewBag.RequirementsComplete = requirementsComplete;

            // Fetch required classes from the Class table
            var requiredClasses = await _context.Classes
                .Where(c => c.IsRequired)
                .Select(c => new
                {
                    c.CategoryPrefix,
                    c.ClassID,
                    c.ClassName
                })
                .ToListAsync();

            ViewBag.RequiredClasses = requiredClasses;

            var requiredClassesList = ViewBag.RequiredClasses as IEnumerable<object>;
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}
