using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Class
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<Child> Childs { get; set; }

        public Class()
        {
            var ListOfStudents = new List<Child>(); 
        }

    }
}
