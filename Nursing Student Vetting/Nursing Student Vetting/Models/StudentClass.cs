using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nursing_Student_Vetting.Models
{
    public class StudentClass
    {
        [Key, Column(Order = 0)]
        public int ClassID { get; set; }

        [Key, Column(Order = 1)]
        public string CategoryPrefix { get; set; }

        [Key, Column(Order = 2)]
        public string StudentID { get; set; }

        public Class? Class { get; set; }

        public Student? Student { get; set; }

        [Required(ErrorMessage = "Please enter a letter grade")]
        public string LetterGrade { get; set; }

    }
}
