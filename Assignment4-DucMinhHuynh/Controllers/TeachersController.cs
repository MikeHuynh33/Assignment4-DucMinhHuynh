using Assignment4_DucMinhHuynh.Models;
using Assignment4_DucMinhHuynh.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Assignment4_DucMinhHuynh.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers
        public ActionResult Index()
        {
            return View();
        }

        //GET: Teacher/List
        [HttpGet]
        [Route("/List")]
        public ActionResult List()
        {
            TeacherDataController teacherController = new TeacherDataController();
            IEnumerable<Teacher> Teachers = teacherController.ListTeachers();
            return View(Teachers);
        }

        [HttpGet]
        [Route("/Show/{id}")]
        public ActionResult Show(int id)
        {
            TeacherDataController teacherController = new TeacherDataController();
            Teacher single_teacher = teacherController.FindTeacher(id);
            return View(single_teacher);
        }

        //POST : /Teacher/Create

        [HttpPost]
        [Route("/Create")]
        public ActionResult Create(string Teacherfname, string Teacherlname, string Employeenumber, DateTime hiredate, Decimal salary)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(Teacherfname);
            Debug.WriteLine(Teacherlname);
            Debug.WriteLine(Employeenumber);
            Debug.WriteLine(hiredate);
            Debug.WriteLine(salary);

            Teacher Newteacher = new Teacher();
            Newteacher.teacherfname = Teacherfname;
            Newteacher.teacherlname = Teacherlname;
            Newteacher.employeeenumber = Employeenumber;
            Newteacher.salary = salary;
            Newteacher.hiredate = hiredate;

            TeacherDataController teachercontroller = new TeacherDataController();
            teachercontroller.AddTeacher(Newteacher);

            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("/CreateTeacher")]
        public ActionResult CreateTeacher()
        {
            return View();
        }
        [HttpGet]
        [Route("/DeleteConfirm")]
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController teacherController = new TeacherDataController();
            Teacher single_teacher = teacherController.FindTeacher(id);
            return View(single_teacher);
        }

        [HttpPost]
        [Route("/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            TeacherDataController teacherController = new TeacherDataController();
            teacherController.RemoveTeacher(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("/AjaxCreateAndDelete")]
        public ActionResult AjaxCreateAndDelete()
        {
            return View();
        }

    }        
}