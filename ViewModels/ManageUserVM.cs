using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SightWordsProject.ViewModels
{
    public class ManageUserVM
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string School { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string AccessCode { get; set; }
        
        [Required]
        public int GradeLevel { get; set; }

        // [Required]
        // [DataType(DataType.Password)]
        // [Display(Name = "New password")]
        // public string Password { get; set; }

        // [DataType(DataType.Password)]
        // [Display(Name = "Confirm password")]
        // [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        // public string ConfirmPassword { get; set;}
    }
}