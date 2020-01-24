using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightWordsProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string School { get; set; }
        public string StudentCode { get; set; }
        public string UserType { get; set; }
        public string StudentId { get; set; }
        public int GradeLevel { get; set; }
    }
}