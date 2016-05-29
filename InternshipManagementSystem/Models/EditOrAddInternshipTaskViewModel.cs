using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class EditOrAddInternshipTaskViewModel
    {
        public List<Class_> classes { get; set; }
        public InternshipTask internshipTask;

        public EditOrAddInternshipTaskViewModel(List<Class_> classes, InternshipTask internshipTask)
        {
            this.internshipTask = internshipTask;
            this.classes = classes;
        }
    }
}