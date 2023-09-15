using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Child
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }


        [Required]
        [PersonalData]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        [Required]
        [PersonalData]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Enrol Date")]
        [DataType(DataType.Date)]
        public DateTime EnrolDate { get; set; }


        [Required]
        public int ClassroomId { get; set; }


        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public virtual Classroom Classroom { get; set; }

        public virtual ICollection<DocumentsCopies> DocumentsImage { get; set; }

        public Child()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}
