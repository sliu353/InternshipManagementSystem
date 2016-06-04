using InternshipManagementSystem.CustomerFilter;
using InternshipManagementSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(thisModel);
        }

        private Company prepareUserSelectedCompanyDetail(string company)
        {
            Company thisCompany = new Company();
            company = company.Replace('-', ' ');
            thisCompany = db.Companies.Where(c => c.CompanyName == company).FirstOrDefault();
            return thisCompany;
        }

        public ActionResult DisplayUserSelectedCompanyDetail(string company)
        {
            return PartialView("_selectedCompany", prepareUserSelectedCompanyDetail(company));
        }

        public ActionResult DisplayUserSelectedCompanyDetailForMobile(string company)
        {
            return PartialView("_selectedCompanyForMobile", prepareUserSelectedCompanyDetail(company));
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
    }
}