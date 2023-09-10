using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime EnrolDate { get; set; }
        public string Image { get; set; }
        
    }
}
