using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;

namespace Registrar.Controllers
{
  public class CoursesController : Controller
  {
    private readonly RegistrarContext _db;
    public CoursesController(RegistrarContext db)
    {
      _db = db;
    }

  public ActionResult Index()
  {
    ViewBag.PageTitle = "View All Courses";
    List<Course> CourseList = _db.Courses.Include(course => course.JoinEntities)
                                          .ThenInclude(join => join.Student)
                                          .OrderByDescending(course => courseJoinEntities.Count)
                                          .ToList();
      return View(CourseList);
    }

    public ActionResult Details(int id)
    {
      Course thisCourse =_db.Course
                              .Include(course => course.JoinEntities)
                              .ThenInclude(join => join.Student)
                              .FirstOrDefault(course => course.CourseId == id);
      ViewBag.PageTitle = $"{thisCourse.Number} Details";
    return View(thisCourse);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add Course";
      return View();
    }

    [HttpPost]
    public ActionResut Create(Course course)
    {
      _db.Courses.Add(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddStudent(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId == id);
      ViewBag.PageTitle = "Add Student";
      return View(thisCourse);
    }

    public ActionResult Edit(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId == id);
      return View(thisCourse);
    }

    [HttpPost]
    public ActionResult Edit(Course course)
    {
      _db.Courses.Update(course);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId == id);
      return View(thisCourse);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Course thisCourse = _db.Courses.FirstOrDefault(courses => courses.CourseId == id);
      _db.Courses.Remove(thisCourse);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}