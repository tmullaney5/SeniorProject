using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SightWordsProject.ViewModels
{
    public class ViewClassVM
    {
        public string StudentId { get; set; }

        public string ParentEmail { get; set; }

        public string ParentFirst { get; set; }
        public string ParentLast { get; set; }

        public string GradeLevel { get; set; }

    }
}