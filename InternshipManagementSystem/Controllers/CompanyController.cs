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
    [RequireHttps]
    public class CompanyController : Controller
    {
        Internship_Management_SystemEntities db = new Internship_Management_SystemEntities();

        [AuthLog(Roles = "company")]
        public ActionResult ForCompany()
        {
            var company = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            return View(company);
        }

        [HttpPost]
        [AuthLog(Roles = "company")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCompanyIntro(Company model)
        {
            var company = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            company.CompanyIntroduction = model.CompanyIntroduction;
            await db.SaveChangesAsync();
            return PartialView("_CompanyIntro", model);
        }

        [HttpPost]
        [AuthLog(Roles = "company")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditInternIntro(Company model)
        {
            var company = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            company.InternIntroduction = model.InternIntroduction;
            await db.SaveChangesAsync();
            return PartialView("_InternIntro", model);
        }

        [HttpPost]
        [AuthLog(Roles = "company")]
        public async Task<ActionResult> MarkStudent(List<string> companyMark)
        {
            var aCopyOfDB = new Internship_Management_SystemEntities();
            var thisCompany = db.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            try {
                var companyMarkIterator = 0;
                foreach (var c in thisCompany.Class_)
                {
                    foreach (var s in c.Students)
                    {
                        int result;
                        if (Int32.TryParse(companyMark[companyMarkIterator], out result))
                            s.CompanyMark = result;
                        else
                            s.CompanyMark = null;
                        companyMarkIterator++;
                    }
                }
                await db.SaveChangesAsync();
            }
            catch(Exception e){
                thisCompany = aCopyOfDB.Companies.Where(c => c.CompanyEmail == User.Identity.Name).FirstOrDefault();
            }
            return PartialView("_CompanyMarkedStudentInfo", thisCompany);
        }
    }
}