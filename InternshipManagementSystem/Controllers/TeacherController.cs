using InternshipManagementSystem.CustomerFilter;
using InternshipManagementSystem.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        Internship_Management_SystemEntities db = new Internship_Management_SystemEntities();


        //This method helps setting up the view model for populating the "ForTeacher" view with needed data.
        private ForTeacherViewModel prepareViewModel()
        {
            var thisTeacher = getThisTeacher();
            ForTeacherViewModel forTeacherModel = new ForTeacherViewModel(thisTeacher);
            if (thisTeacher.Class_ != null)
            {
                forTeacherModel.Classes = thisTeacher.Class_.ToList();
                forTeacherModel.Students = new List<Student>();
                foreach (var c in forTeacherModel.Classes)
                {
                    forTeacherModel.Students.AddRange(c.Students.ToList());
                }
            }
            if (thisTeacher.Contract_ != null)
            {
                forTeacherModel.Contracts = thisTeacher.Contract_.ToList();
            }
            forTeacherModel.AllCompanies = db.Companies.ToList();

            return forTeacherModel;
        }

        [AuthLog(Roles = "teacher")]
        public ActionResult ForTeacher()
        {
            ViewBag.Message = "This page is for teacher";
            return View(prepareViewModel());
        }

        private Teacher getThisTeacher()
        {
            return db.Teachers.Where(t => t.TeacherEmail == User.Identity.Name).FirstOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult> UploadStudentInfo(FormCollection formCollection)
        {
            var thisTeacher = getThisTeacher();
            if (Request != null)
            {
                //Create a copy of db so if user submit any files with problem that mess up db, it can be reverted back.
                var aCopyOfDB = new Internship_Management_SystemEntities();
                try
                {
                    //If there are already records of classes and students that are related to this teacher, delete them and replace with our new file.
                    db.Students.RemoveRange(thisTeacher.Students);
                    db.Class_.RemoveRange(thisTeacher.Class_);
                    HttpPostedFileBase file = Request.Files["UploadedFile"];

                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    }
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        Class_ class_ = new Class_();
                        for (int rowIterator = 2; ; rowIterator++)
                        {
                            if (workSheet.Cells[rowIterator, 1].Value != null)
                            {
                                //Calculate the number of students in this specific class.
                                if (class_.Students.Count != 0)
                                {
                                    class_.NumberOfStudents = class_.Students.Count;
                                    db.Class_.Add(class_);
                                }
                                class_ = new Class_();
                                class_.Teacher = thisTeacher;
                                class_.TeacherEmail = thisTeacher.TeacherEmail;
                                class_.ClassName = workSheet.Cells[rowIterator, 1].Value.ToString();

                            }
                            try
                            {
                                Student student = new Student();
                                student.Class_ = class_;
                                student.ClassName = class_.ClassName;
                                student.StudentName = workSheet.Cells[rowIterator, 2].Value.ToString();
                                student.PhoneNumber = workSheet.Cells[rowIterator, 3].Value.ToString();
                                student.TeacherEmail = thisTeacher.TeacherEmail;
                                student.Teacher = thisTeacher;
                                db.Students.Add(student);
                            }
                            catch (NullReferenceException ex)
                            {
                                class_.NumberOfStudents = class_.Students.Count;
                                break;
                            }
                        }
                    }
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    db = aCopyOfDB;
                    ViewData["isSuccessful"] = false;
                    return View("ForTeacher", prepareViewModel());
                }
                ViewData["isSuccessful"] = true;
            }
            return View("ForTeacher", prepareViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> MarkStudent(List<string> teacherMark)
        {
            var thisTeacher = getThisTeacher();
            var teacherMarkIterator = 0;
            foreach (var c in thisTeacher.Class_)
            {
                foreach (var s in c.Students)
                {
                    int result;
                    if (Int32.TryParse(teacherMark[teacherMarkIterator], out result))
                        s.TeacherMark = result;
                    else
                        s.TeacherMark = null;
                    teacherMarkIterator++;
                }
            }
            await db.SaveChangesAsync();
            return PartialView("_MarkedStudentInfo", prepareViewModel());
        }

        [HttpPost]
        public ActionResult EditOrAddContract(Contract_ contract, List<string> Classes, string company)
        {
            var thisTeacher = getThisTeacher();
            var aCopyOfDatabase = new Internship_Management_SystemEntities();

            try
            {
                // If there is already a contract between this teacher and this company, delete it and replace it with our new contract later.
                db.Contract_.RemoveRange(db.Contract_.Where(c => c.CompanyName == company && c.TeacherEmail == thisTeacher.TeacherEmail));
                // If there are any contract that related to the classes we get, we also need to delete them.
                db.Contract_.RemoveRange(db.Contract_.Where(c => (from classes in db.Class_.Where(cl => Classes.Contains(cl.ClassName)) select classes.CompanyName).Contains(c.CompanyName) && c.TeacherEmail == thisTeacher.TeacherEmail));
                contract.Teacher = thisTeacher;
                contract.TeacherEmail = thisTeacher.TeacherEmail;
                contract.Company = db.Companies.Where(c => c.CompanyName == company).FirstOrDefault();
                contract.Teacher = db.Teachers.Where(t => t.TeacherEmail == contract.TeacherEmail).FirstOrDefault();
                db.Contract_.Add(contract);
                db.SaveChanges();

                var contractedCompany = db.Companies.Where(c => c.CompanyName == contract.CompanyName).FirstOrDefault();
                foreach (string class_ in Classes)
                {
                    db.Class_.Where(c => c.ClassName == class_).FirstOrDefault().Company = contract.Company;
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                db = aCopyOfDatabase;
            }
            return PartialView("_Contracts", prepareViewModel());
        }
    }
}