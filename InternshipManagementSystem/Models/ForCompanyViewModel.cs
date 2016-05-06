using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class ForCompanyViewModel
    {
        public string CompanyName { get; set; }
        public string InternIntroduction { get; set; }
        public string CompanyIntroduction { get; set; }
        public List<Student> Students {get; set;}

        public ForCompanyViewModel(){}

        public ForCompanyViewModel(string companyName, string internIntroduction, string companyIntroduction, List<Student> students)
        {
            CompanyName = companyName;
            InternIntroduction = internIntroduction;
            CompanyIntroduction = companyIntroduction;
            Students = students;
        }
    }
}