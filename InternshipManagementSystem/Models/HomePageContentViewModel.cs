using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class HomePageContentViewModel
    {
        public List<HomePageContent> HomePageContents { get; set; }

        public HomePageContentViewModel(List<HomePageContent> homePageContents)
        {
            HomePageContents = homePageContents;
        }
    }
}