﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Preschool.Models
{
    public class Teacher
    {
        [Required]
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
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required]
        public DateTime RegistedAt { get; set; }

        public bool  IsActive { get; set; }

        [Required]
        public int ClassId { get; set; } 

        public virtual Class Class { get; set; }

    }
}