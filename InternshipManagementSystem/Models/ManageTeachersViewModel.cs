using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class ManageTeachersViewModel
    {
        public List<Teacher> Teachers { get; set; }

        public ManageTeachersViewModel(List<Teacher> teachers)
        {
            Teachers = teachers;
        }
    }
}