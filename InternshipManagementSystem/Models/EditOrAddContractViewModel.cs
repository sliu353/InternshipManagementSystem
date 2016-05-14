using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipManagementSystem.Models
{
    public class EditOrAddContractViewModel
    {
        public Contract_ contract { get; set; }
        public List<Class_> classes { get; set; }
        public List<Company> allCompanies { get; set; }

        public EditOrAddContractViewModel() { }

        public EditOrAddContractViewModel(Contract_ contract, List<Class_> classes, List<Company> companies)
        {
            this.contract = contract;
            this.classes = classes;
            this.allCompanies = companies;
        }
    }
}