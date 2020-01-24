using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SightWordsProject.Models;

namespace SightWordsProject.ViewModels
{
    public class CreateAccountVM
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
         public string LastName { get; set; }

         public string School { get; set; }

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

         public string StudentCode { get; set; }

        [Required]
        public int GradeLevel { get; set; }
      
    }
}