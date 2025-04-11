using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nursing_Student_Vetting.Models
{
    public class Class
    {
        [Key, Column(Order = 0 )]
        public int ClassID { get; set; } // primary key
        [Key, Column(Order = 1)]
        public string CategoryPrefix { get; set; } // foreign key
        public ClassCategories? Category { get; set; }

        [Required(ErrorMessage = "Please enter a class name")]
        public string ClassName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter number of credit hours")]
        public int CreditHours { get; set; }


        public bool IsRequired { get; set; }

        public ICollection<StudentClass>? StudentClasses { get; set; }
    }
}
