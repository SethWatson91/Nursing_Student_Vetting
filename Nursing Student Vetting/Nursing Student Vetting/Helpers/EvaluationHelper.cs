namespace Nursing_Student_Vetting.Helpers
{
    public class EvaluationHelper
    {
        private readonly NursingStudentContext _context;
        private readonly TestHelper _testHelper;
        private readonly GradeHelper _gradeHelper;
        private readonly RequirementsHelper _requirementsHelper;

        public EvaluationHelper(NursingStudentContext context, TestHelper testHelper, GradeHelper gradeHelper, RequirementsHelper requirementsHelper)
        {
            _context = context;
            _testHelper = testHelper;
            _gradeHelper = gradeHelper;
            _requirementsHelper = requirementsHelper;
        }

        public async Task<int> CalculateEvaluationScore(string studentID)
        {
            var totalScore = 0;

            // GPA score calculation
            var GPA = await _gradeHelper.GetGPA(studentID);
            totalScore += (int)(GPA * 1000);

            // ACT score calculation
            var studentTests = _context.StudentTests
                .Where(st => st.StudentID == studentID)
                .ToList();

            foreach (var studentTest in studentTests)
            {
                if (IsRecentTestAttempt(studentTest.AttemptDate))
                {
                    int actScore = 0;
                    int designatedTestScore = 0;

                    // Calculate ACT score if it's an ACT test
                    if (studentTest.Test.TestName == "ACT")
                    {
                        actScore = _testHelper.CalculateACTScore(studentTest.Score);
                    }
                    // Calculate Designated Test score if it's a Designated Test
                    else if (studentTest.Test.TestName == "Designated Test")
                    {
                        designatedTestScore = _testHelper.CalculateDesignatedTestScore(studentTest.Score, studentTest.AttemptNumber);
                    }

                    // Add both scores together
                    totalScore += actScore + designatedTestScore;
                }
            }

            bool preReqsComplete = _requirementsHelper.AreRequirementsMet(studentID);
            totalScore += preReqsComplete ? 500 : 0;

            // Extra Credit Hours Calculation (Using RequirementsHelper)
            var completedCreditHours = await _requirementsHelper.GetCreditHoursPreReqs(studentID);
            if (completedCreditHours >= 27)
            {
                totalScore += 1000;
            }
            else if (completedCreditHours >= 12)
            {
                totalScore += 500;
            }

            // Biology 2010 and 2020 Grade Checks (Using RequirementsHelper)
            var biology2010Grade = await _requirementsHelper.GetGradeForCourse(studentID, "BIOL", 2010);
            var biology2020Grade = await _requirementsHelper.GetGradeForCourse(studentID, "BIOL", 2020);

            if (biology2010Grade == "A")
            {
                totalScore += 500;
            }
            else if (biology2010Grade == "B")
            {
                totalScore += 250;
            }

            if (biology2020Grade == "A")
            {
                totalScore += 500;
            }
            else if (biology2020Grade == "B")
            {
                totalScore += 250;
            }

            return totalScore;
        }

        // Helper method to check if the test attempt was within the last 5 years
        private bool IsRecentTestAttempt(DateTime? attemptDate)
        {
            if (attemptDate.HasValue)
            {
                return attemptDate.Value.CompareTo(DateTime.Now.AddYears(-5)) > 0;
            }
            return false;
        }

    }
}
