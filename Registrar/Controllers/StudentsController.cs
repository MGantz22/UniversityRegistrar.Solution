using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
    public class StudentController : Controller
    {
        private readonly RegistrarContext _db;

        public StudentsController(RegistrarContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Student> model = _db.Students
                                    .Include(student => student.Course)
                                    .ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (student.CourseId == 0)
            {
                return RedirectToAction("Create");
            }
            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Student thisStudent = _db.Students
                .Include(student => student.Course)
                .Include(student => student.JoinEntities)
                .ThenInclude(join => join.Tag)
                .FirstOrDefault(student => student.StudentId == id);
            return View(thisStudent);
        }

        public ActionResult Edit(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");
            return View(thisStudent);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            _db.Students.Update(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            return View(thisStudent);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(student => student.StudentsId == id);
            _db.Students.Remove(thisStudent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
