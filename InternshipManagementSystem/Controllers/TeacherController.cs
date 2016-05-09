using InternshipManagementSystem.CustomerFilter;
using InternshipManagementSystem.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InternshipManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        Internship_Management_SystemEntities db = new Internship_Management_SystemEntities();

        //This method helps set up the view model for populating the "ForTeacher" view with needed data.
        private ForTeacherViewModel prepareViewModel()
        {
            var thisTeacher = db.Teachers.Where(t => t.TeacherEmail == User.Identity.Name).FirstOrDefault();
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
            return forTeacherModel;
        }

        [AuthLog(Roles = "teacher")]
        public ActionResult ForTeacher()
        {
            ViewBag.Message = "This page is for teacher";
            return View(prepareViewModel());
        }

        public async Task<ActionResult> UploadStudentInfo(FormCollection formCollection)
        {
            var thisTeacher = db.Teachers.Where(t => t.TeacherEmail == User.Identity.Name).FirstOrDefault();
            if (Request != null)
            {
                try
                {
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
                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
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
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    ViewData["isSuccessful"] = false;
                }
                ViewData["isSuccessful"] = true;
            }
            return View("ForTeacher", prepareViewModel());
        }
    }
}