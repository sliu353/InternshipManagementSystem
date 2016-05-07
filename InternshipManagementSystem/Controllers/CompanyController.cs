using InternshipManagementSystem.CustomerFilter;
using InternshipManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class CompanyController : Controller
    {
        Internship_Management_SystemEntities db = new Internship_Management_SystemEntities();

        [AuthLog(Roles = "company")]
        public ActionResult ForCompany()
        {
            var company = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            ViewBag.Message = "This page is for company";
            var students = (from theStudent in db.Students where theStudent.Company.CompanyName == company.CompanyName select theStudent).ToList();
            var companyIntroduction = company.CompanyIntroduction;
            var internIntroduction = company.InternIntroduction;
            string companyName = company.CompanyName;

            // Create our view model.
            ForCompanyViewModel ForCompanyModel = new ForCompanyViewModel(companyName, internIntroduction, companyIntroduction, students);
            return View(ForCompanyModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCompanyIntro(ForCompanyViewModel model)
        {
            var company = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            company.CompanyIntroduction = model.CompanyIntroduction;
            await db.SaveChangesAsync();
            return PartialView("_CompanyIntro", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditInternIntro(ForCompanyViewModel model)
        {
            var company = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            company.InternIntroduction = model.InternIntroduction;
            await db.SaveChangesAsync();
            return PartialView("_InternIntro", model);
        }
    }
}