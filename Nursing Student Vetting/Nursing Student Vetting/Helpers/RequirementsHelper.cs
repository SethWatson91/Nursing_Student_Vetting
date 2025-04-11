using Microsoft.EntityFrameworkCore;
using Nursing_Student_Vetting.Models;


namespace Nursing_Student_Vetting.Helpers
{
    public class RequirementsHelper
    {
        private readonly NursingStudentContext _context;
        public RequirementsHelper(NursingStudentContext context)
        {
            _context = context;
        }

        private readonly List<Class> humanities = new List<Class>
        {
            new Class {CategoryPrefix = "ART" , ClassID = 1035},
            new Class {CategoryPrefix = "ART" , ClassID = 2000},
            new Class {CategoryPrefix = "ART" , ClassID = 2020},
            new Class {CategoryPrefix = "EGRT", ClassID = 2020},
            new Class {CategoryPrefix = "ENGL", ClassID = 2110},
            new Class {CategoryPrefix = "ENGL", ClassID = 2120},
            new Class {CategoryPrefix = "ENGL", ClassID = 2210},
            new Class {CategoryPrefix = "ENGL", ClassID = 2220},
            new Class {CategoryPrefix = "ENGL", ClassID = 2410},
            new Class {CategoryPrefix = "ENGL", ClassID = 2420},
            new Class {CategoryPrefix = "HUM" , ClassID = 2860},
            new Class {CategoryPrefix = "HUM" , ClassID = 1010},
            new Class {CategoryPrefix = "HUM" , ClassID = 1020},
            new Class {CategoryPrefix = "MUS" , ClassID = 1030},
            new Class {CategoryPrefix = "PHIL", ClassID = 1030},
            new Class {CategoryPrefix = "PHIL", ClassID = 2020},
            new Class {CategoryPrefix = "PHIL", ClassID = 2200},
            new Class {CategoryPrefix = "PHIL", ClassID = 1040},
            new Class {CategoryPrefix = "PHIL", ClassID = 2640},
            new Class {CategoryPrefix = "THEA", ClassID = 1030},
            new Class {CategoryPrefix = "RELS", ClassID = 2020},
            new Class {CategoryPrefix = "SPAN", ClassID = 1030},
        };


        public bool AreRequirementsMet(string studentId)
        {
            // Get all required classes
            var requiredClasses = _context.Classes
                .Where(c => c.IsRequired)
                .ToList();



            // Get the student's completed classes with grades
            var studentClasses = _context.StudentClasses
                .Where(sc => sc.StudentID == studentId)
                .Include(sc => sc.Class)  // Load Class data
                .ToList();


            // Check if every required class is completed with a passing grade
            bool allRequiredClassesCompleted = requiredClasses.All(requiredClass =>
            {
                var studentClass = studentClasses.FirstOrDefault(sc => sc.Class.ClassID == requiredClass.ClassID);
                return studentClass != null && studentClass.LetterGrade != null && studentClass.LetterGrade != "F";
            });

            return allRequiredClassesCompleted && IsHumanitiesComplete(studentId);
        }

        public bool IsHumanitiesComplete(string studentId)
        {
            return _context.StudentClasses
                .Where(sc => sc.StudentID == studentId && sc.LetterGrade != null && sc.LetterGrade != "F")
                .AsEnumerable()
                .Any(sc => humanities.Any(h => h.ClassID == sc.ClassID && h.CategoryPrefix == sc.CategoryPrefix));
        }

        public async Task<int> GetCreditHoursPreReqs(string studentID)
        {
            var completedClasses = await _context.StudentClasses
                .Where(sc => sc.StudentID == studentID)
                .ToListAsync();

            var totalCreditHours = completedClasses.Sum(sc => sc.Class.CreditHours);

            var preReqClasses = await _context.StudentClasses
                .Where(sc => sc.StudentID == studentID && sc.Class.IsRequired == true)
                .ToListAsync();

            int preReqCreditHours = preReqClasses.Sum(sc => sc.Class.CreditHours);

            return totalCreditHours - preReqCreditHours;
        }

        public async Task<string> GetGradeForCourse(string studentID, string categoryPrefix, int classId)
        {
            var course = await _context.StudentClasses
                .Where(sc => sc.StudentID == studentID && sc.Class.CategoryPrefix == categoryPrefix && sc.Class.ClassID == classId)
                .FirstOrDefaultAsync();

            return course?.LetterGrade;
        }

    }

}
