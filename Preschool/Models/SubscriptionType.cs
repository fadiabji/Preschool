using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class SubscriptionType
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Duration Month")]
        public int DurationMonth { get; set; }

        [Required]
        public decimal Price { get; set; }


        public virtual ICollection<Subscription> Supscriptions { get; set; }
    }
}
