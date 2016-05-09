using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class ForTeacherViewModel
    {
        public Teacher ThisTeacher { get; set; }
        public List<Class_> Classes { get; set; }
        public List<Student> Students { get; set; }

        public ForTeacherViewModel() { }

        public ForTeacherViewModel(Teacher thisTeacher)
        {
            ThisTeacher = thisTeacher;
        }
    }
}