using Microsoft.EntityFrameworkCore;
using Nursing_Student_Vetting.Models;

namespace Nursing_Student_Vetting.Helpers
{
    public class GradeHelper
    {
        private readonly NursingStudentContext _context;
        public GradeHelper(NursingStudentContext context)
        {
            _context = context;
        }


        public async Task<decimal> GetGPA(string studentID)
        {
            decimal GPA = 0.0m;
            decimal totalCreditHours = 0.0m;
            decimal totalQualityPoints = 0.0m;
            try
            {
                var classes = await _context.StudentClasses
                    .Where(sc => sc.StudentID == studentID)
                    .Include(sc => sc.Class)
                    .ToListAsync();

                //calculate GPA
                foreach (var course in classes)
                {
                    if (course.LetterGrade == null)
                    {
                        throw new Exception();
                    }

                    totalQualityPoints += GetQualityPoints(
                                            GetGradeValue(course.LetterGrade), course.Class.CreditHours);
                    totalCreditHours += course.Class.CreditHours;
                    
                }
                GPA = totalQualityPoints / totalCreditHours;
            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception($"Error calculating GPA", ex);
            }
            return GPA;
        }


        public decimal GetQualityPoints(decimal gradeValue, int creditHours)
        {
            return gradeValue * creditHours;
        }


        public decimal GetGradeValue(string letterGrade)
        {
            switch (letterGrade.ToUpper())
            {
                case "A": return 4.0m;
                case "B": return 3.0m;
                case "C": return 2.0m;
                case "D": return 1.0m;
                case "F": return 0.0m;
                default: return 0.0m;
            }
        }
    }
}
