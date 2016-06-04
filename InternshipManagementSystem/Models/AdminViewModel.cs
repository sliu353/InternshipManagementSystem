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

        public AdminViewModel(List<HomePageContent> homePageContents, List<Company> companies, List<Teacher> teachers)
        {
            HomePageContents = homePageContents;
            Companies = companies;
            Teachers = teachers;
        }

        public AdminViewModel()
        {

        }
    }
}