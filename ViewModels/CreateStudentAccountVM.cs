using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SightWordsProject.Utilities;

namespace SightWordsProject.ViewModels
{
    public class CreateStudentAccountVM
    {
         [Required]
         [Display(Name ="Parent First Name")]
        public string ParentFirst { get; set; }

        [Required]
        [Display(Name = "Parent Last Name")]
         public string ParentLast { get; set; }
        
        [Required]
        [MaxLength(8)]
        public string AccessCode { get; set; }

        [Required]
        public string StudentId { get; set; }

         [Required]
         [EmailAddress]
         public string Email { get; set; }

         [Required]
         [DataType(DataType.Password)]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Confirm password")]
         [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
         public string ConfirmPassword { get; set;}
      
    }
}