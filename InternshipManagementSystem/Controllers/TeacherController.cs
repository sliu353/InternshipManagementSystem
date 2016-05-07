using InternshipManagementSystem.CustomerFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        [AuthLog(Roles = "teacher")]
        public ActionResult ForTeacher()
        {
            ViewBag.Message = "This page is for teacher";
            return View();
        }
    }
}