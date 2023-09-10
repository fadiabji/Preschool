using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Preschool.Models
{
    public class DocumentsImage
    {
            [Key]
            [Required]
            public int Id { get; set; }

            [Required]
            public string ImageFile { get; set; }

            [ForeignKey("Child")]
            public int ChildId { get; set; }

            public virtual Child Child { get; set; }

        
    }
}
