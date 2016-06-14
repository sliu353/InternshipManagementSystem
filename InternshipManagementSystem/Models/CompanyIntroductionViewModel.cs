using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class CompanyIntroductionViewModel
    {
        public List<Company> targetedCompanies { get; set; }
        public List<Company> companies { get; set; }
        public string keyWords { get; set; }
    }
}