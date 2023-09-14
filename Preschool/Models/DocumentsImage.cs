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

            [Required]
            public int ChildId { get; set; }

            public virtual Child Child { get; set; }

            [Required]
            public int TeacherId { get; set; }

            public virtual Teacher Teacher { get; set; }


    }
}
