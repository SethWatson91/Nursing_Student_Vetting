using Nursing_Student_Vetting.Models;
using System.Linq;

namespace Nursing_Student_Vetting.Helpers
{
    public class TestHelper
    {
        private readonly NursingStudentContext _context;

        // Inserting the context into the class
        public TestHelper(NursingStudentContext context)
        {
            _context = context;
        }

        // Method to calculate ACT test points based on the score
        public int CalculateACTScore(int score)
        {
            if (score >= 26) return 500;
            if (score >= 19) return 250;
            return 0;
        }

        // Method to calculate points for designated tests based on score and attempt number
        public int CalculateDesignatedTestScore(int score, int attemptNumber)
        {
            if (attemptNumber == 1 && score >= 80) return 500;
            if (attemptNumber == 2 && score >= 80) return 250;
            return 0;
        }         
    }    
}
