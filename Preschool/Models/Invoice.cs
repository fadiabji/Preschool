using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Invoice
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
