using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Nursing_Student_Vetting.Models
{
    public class Student
    {

        [Key]
        [Required(ErrorMessage = "W number is required")]
        public string StudentID { get; set; }
        public int EvaluationScore { get; set; }


        [Required(ErrorMessage = "Please enter a first name")]
        public String FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter a last name")]
        public String LastName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter an email")]
        public String Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter an address")]
        public String Address { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter a date of birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please enter a gender")]
        public string Gender { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter a starting date")]
        public DateTime? StartDate { get; set; } = DateTime.Now;

        public DateTime? GraduationDate { get; set; }

        public ICollection<StudentTest> StudentTests { get; set; } = new List<StudentTest>();
        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
    }
}
