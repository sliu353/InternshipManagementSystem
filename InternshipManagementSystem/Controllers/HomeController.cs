using InternshipManagementSystem.CustomerFilter;
using InternshipManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        Internship_Management_SystemEntities db = new Internship_Management_SystemEntities();

        public ActionResult Index()
        {
            return View(new HomePageContentViewModel(db.HomePageContents.ToList()));
        }

        public ActionResult InternshipTaskIntroduction()
        {
            List<Teacher> teachers = db.Teachers.ToList();
            List<InternshipTask> internshipTasks = db.InternshipTasks.ToList();
            List<Class_> classes = db.Class_.ToList();
            return View(new InternshipTaskIntroductionViewModel(internshipTasks, classes, teachers));
        }

        public ActionResult CompanyIntroduction()
        {
            CompanyIntroductionViewModel thisModel = new CompanyIntroductionViewModel();
            thisModel.companies = db.Companies.ToList();
            thisModel.targetedCompanies = new List<Company>();
            return View(thisModel);
        }

        private Company prepareUserSelectedCompanyDetail(string company, string keyWords)
        {
            Company thisCompany = new Company();
            company = company.Replace('-', ' ');
            thisCompany = db.Companies.Where(c => c.CompanyName == company).FirstOrDefault();
            if (keyWords != null)
            {
                var replacedKeyWords = "<span class=\"keyWord\">" + keyWords + "</span>";
                thisCompany.CompanyIntroduction = thisCompany.CompanyIntroduction.Replace(keyWords, replacedKeyWords);
                thisCompany.CompanyLocation = thisCompany.CompanyLocation.Replace(keyWords, replacedKeyWords);
                thisCompany.CompanyName = thisCompany.CompanyName.Replace(keyWords, replacedKeyWords);
                thisCompany.InternIntroduction = thisCompany.InternIntroduction.Replace(keyWords, replacedKeyWords);
                thisCompany.PersonInCharge = thisCompany.PersonInCharge.Replace(keyWords, replacedKeyWords);
                thisCompany.ContactNumber = thisCompany.ContactNumber.Replace(keyWords, replacedKeyWords);
            }
            return thisCompany;
        }

        public ActionResult DisplayUserSelectedCompanyDetail(string company, string keyWords)
        {
            return PartialView("_selectedCompany", prepareUserSelectedCompanyDetail(company, keyWords));
        }

        public ActionResult DisplayUserSelectedCompanyDetailForMobile(string company, string keyWords)
        {
            return PartialView("_selectedCompanyForMobile", prepareUserSelectedCompanyDetail(company, keyWords));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Grade()
        {
            GradeViewModel thisModel = new GradeViewModel();
            thisModel.Classes = db.Class_.ToList();
            return View(thisModel);
        }

        public ActionResult Search(string keyWords)
        {
            // Replace keyword with a "<span>" so that it can be highlighted.
            var replacedKeyWords = "<span class=\"keyWord\">" + keyWords + "</span>";

            CompanyIntroductionViewModel thisModel = new CompanyIntroductionViewModel();

            // Select the companies with the keywords we need.
            var targetedCompanies = db.Companies.Where(c => c.CompanyIntroduction.Contains(keyWords) || c.CompanyLocation.Contains(keyWords) || c.CompanyName.Contains(keyWords) || c.InternIntroduction.Contains(keyWords) || c.PersonInCharge.Contains(keyWords) || c.ContactNumber.Contains(keyWords)).ToList();

            // Replace keywords with the "replacedKeyWords".
            targetedCompanies.ForEach(c => 
            {
                c.CompanyIntroduction = c.CompanyIntroduction.Replace(keyWords, replacedKeyWords);
                c.CompanyLocation = c.CompanyLocation.Replace(keyWords, replacedKeyWords);
                c.CompanyName = c.CompanyName.Replace(keyWords, replacedKeyWords);
                c.InternIntroduction = c.InternIntroduction.Replace(keyWords, replacedKeyWords);
                c.PersonInCharge = c.PersonInCharge.Replace(keyWords, replacedKeyWords);
                c.ContactNumber = c.ContactNumber.Replace(keyWords, replacedKeyWords);
                }
            );
            thisModel.targetedCompanies = targetedCompanies;
            thisModel.companies = db.Companies.ToList().Except(targetedCompanies).ToList();
            thisModel.keyWords = keyWords;
            return View("CompanyIntroduction", thisModel);
        }
    }
}