using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class ManageCompaniesViewModel
    {
        public List<Company> Companies { get; set; }

        public ManageCompaniesViewModel(List<Company> companies)
        {
            Companies = companies;
        }
    }
}