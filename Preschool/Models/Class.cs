using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Class
    {
        [Required]

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Student> Students { get; set; }

    }
}
