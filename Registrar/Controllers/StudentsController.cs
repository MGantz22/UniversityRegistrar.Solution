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

        public StudentsController(Registrar db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Student> model = _db.Items
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

  


    }
}
