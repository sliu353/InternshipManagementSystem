using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class AdminViewModel
    {
        public List<HomePageContent> HomePageContents { get; set; }
        public List<Company> Companies { get; set; }
        public List<Teacher> Teachers { get; set; }

        public AdminViewModel(){
            var db = new Internship_Management_SystemEntities();
            HomePageContents = db.HomePageContents.ToList();
            Companies = db.Companies.ToList();
            Teachers = db.Teachers.ToList();
        }
    }
}