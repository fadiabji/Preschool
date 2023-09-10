using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Preschool.Models.ViewModels
{
    public class ChildVM
    {
        
        [Key]
        public int Id { get; set; }

        
       
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        
       
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
       
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }


        
       
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

        
       
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime EnrolDate { get; set; }

        //public string Image { get; set; }


        public bool IsActive { get; set; } = true;


        public List<string> DocumentCopies { get; set; }

    }
}
