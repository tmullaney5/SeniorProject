using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SightWordsProject.ViewModels
{
    public class StudentDashboard
    {
        public string Id { get; set; }
        public int Gradelevel { get; set; }
        public int Modules { get; set; }
        public int ModuleNumber { get; set; }

    }
}