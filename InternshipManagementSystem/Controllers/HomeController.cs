using InternshipManagementSystem.CustomerFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Grade()
        {
            ViewBag.Message = "Your Grade";
            return View();
        }

        [AuthLog(Roles = "teacher")]
        public ActionResult ForTeacher()
        {
            ViewBag.Message = "This page is for teacher";
            return View();
        }

        [AuthLog(Roles = "company")]
        public ActionResult ForCompany()
        {
            ViewBag.Message = "This page is for company";
            return View();
        }
    }
}