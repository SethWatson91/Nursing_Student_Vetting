using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nursing_Student_Vetting.Models
{
    public class StudentTest
    {
        [Key, Column(Order = 0)]
        public int TestID { get; set; }
        
        [Key, Column(Order = 1)]
        public int AttemptNumber { get; set; }

        [Key, Column(Order = 2)]
        public string StudentID { get; set; }
        
        public Test? Test { get; set; }

        public Student? Student {  get; set; }

        [Required(ErrorMessage = "Please enter a test score")]
        public int Score { get; set; }

        public DateTime AttemptDate { get; set; }
    }
}
