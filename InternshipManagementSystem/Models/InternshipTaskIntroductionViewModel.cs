using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class InternshipTaskIntroductionViewModel
    {
        public List<InternshipTask> InternshipTasks { get; set; }
        public List<Class_> Classes { get; set; }
        public List<Teacher> Teachers { get; set; }

        public InternshipTaskIntroductionViewModel(List<InternshipTask> internshipTasks, List<Class_> classes, List<Teacher> teachers)
        {
            InternshipTasks = internshipTasks;
            Classes = classes;
            Teachers = teachers;
        }
    }
}