using InternshipManagementSystem.CustomerFilter;
using InternshipManagementSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        Internship_Management_SystemEntities db = new Internship_Management_SystemEntities();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
        AdminViewModel adminViewModel;

        // GET: Admin
        [AuthLog(Roles = "admin")]
        public ActionResult ForAdmin()
        {
            adminViewModel = new AdminViewModel(db.HomePageContents.ToList(), db.Companies.ToList(), db.Teachers.ToList());
            return View(adminViewModel);
        }

        [AuthLog(Roles = "admin")]
        public async Task<ActionResult> EditOrAddHomePageContent(HomePageContent homePageContent)
        {
            var aCopyOfDatabase = new Internship_Management_SystemEntities();

            if (db.HomePageContents.Any(h => h.HomePageContentId == homePageContent.HomePageContentId))
            {
                var thisHomePageContent = db.HomePageContents.Where(h => h.HomePageContentId == homePageContent.HomePageContentId).FirstOrDefault();
                thisHomePageContent.Content = homePageContent.Content;
                thisHomePageContent.Title = homePageContent.Title;
            }
            else
            {
                db.HomePageContents.Add(homePageContent);
            }
            try {
                await db.SaveChangesAsync();
            }catch(Exception e)
            {
                db = aCopyOfDatabase;
            }
            return PartialView("_HomePageContents", new HomePageContentViewModel(db.HomePageContents.ToList()));
        }

        [AuthLog(Roles = "admin")]
        public async Task<ActionResult> DeleteHomePageContent(int homePageContentId)
        {
            db.HomePageContents.Remove(db.HomePageContents.Where(h => h.HomePageContentId == homePageContentId).FirstOrDefault());
            await db.SaveChangesAsync();
            return View("ForAdmin", new AdminViewModel(db.HomePageContents.ToList(), db.Companies.ToList(), db.Teachers.ToList()));
        }

        [AuthLog(Roles = "admin")]
        public async Task<ActionResult> SortCompanies(List<string> companiesInNewOrder)
        {
            int companyCounter = 0;
            foreach(var company in companiesInNewOrder)
            {
                companyCounter++;
                db.Companies.Where(c => c.CompanyName == company).FirstOrDefault().CompanyOrder = companyCounter;
            }
            await db.SaveChangesAsync();
            return View("ForAdmin", new AdminViewModel(db.HomePageContents.ToList(), db.Companies.ToList(), db.Teachers.ToList()));
        }

        public ActionResult CancelSortingCompanies()
        {
            return View("ForAdmin", new AdminViewModel(db.HomePageContents.ToList(), db.Companies.ToList(), db.Teachers.ToList()));
        }

        public async Task<ActionResult> DeleteCompany(string companyToBeDeletedName)
        {
            var companyToBeDeleted = db.Companies.Where(c => c.CompanyName == companyToBeDeletedName).FirstOrDefault();
            db.Students.Where(s => s.CompanyName == companyToBeDeletedName).ToList().ForEach(s => { s.CompanyMark = null; s.Company = null; });
            db.Class_.Where(c => c.CompanyName == companyToBeDeletedName).ToList().ForEach(c => c.Company = null);
            db.Contract_.RemoveRange(db.Contract_.Where(c => c.CompanyName == companyToBeDeletedName));
            var companyToBeDeletedInAuthenticationDb = userManager.Users.Where(u => u.Email == companyToBeDeleted.CompanyEmail).FirstOrDefault();
            await userManager.DeleteAsync(companyToBeDeletedInAuthenticationDb);
            db.Companies.Remove(companyToBeDeleted);
            var ReorderCounter = 0;
            db.Companies.OrderBy(c => c.CompanyOrder).ToList().ForEach(c => { ReorderCounter++; c.CompanyOrder = ReorderCounter; });
            await db.SaveChangesAsync();
            return View("ForAdmin", new AdminViewModel(db.HomePageContents.ToList(), db.Companies.ToList(), db.Teachers.ToList()));
        }
    }
}