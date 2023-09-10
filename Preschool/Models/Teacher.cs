using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Teacher
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
        public DateTime Registedat { get; set; }
        public Class Class { get; set; }

    }
}
