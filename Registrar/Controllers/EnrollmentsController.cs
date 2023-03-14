using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Registrar.Controllers
{
  public class EnrollmentsController : Controller
  {
    private readonly RegistrarContext _db;

    public EnrollmentsController(RegistrarContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Enrollments.ToList());
    }

    public ActionResult Details(int id)
    {
      Enrollment thisEnrollment = _db.Enrollments
        .Include(enrollment => enrollment.JoinEntities)
        .ThenInclude(join => join.Student)
        .FirstOrDefault(enrollment => enrollment.EnrollmentId == id);
      return View(thisEnrollment);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Enrollment enrollment)
    {
      _db.Enrollments.Add(enrollment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddStudent(int id)
    {
      Enrollment thisEnrollment = _db.Enrollments.FirstOrDefault(enrollments => enrollments.EnrollmentId == id);
      ViewBag.StudentId = new SelectList(_db.Students, "StudentId", "Description");
      return View(thisEnrollment);
    }

    [HttpPost]
    public ActionResult AddStudent(Enrollment enrollment, int studentId)
    {
      #nullable enable
      StudentEnrollment? joinEntity = _db.StudentEnrollments.FirstOrDefault(join => (join.StudentId == studentId && join.EnrollmentId == enrollment.EnrollmentId));
      #nullable disable
      if (joinEntity == null && studentId != 0)
      {
        _db.StudentEnrollments.Add(new StudentEnrollment() { StudentId = studentId, EnrollmentId = enrollment.EnrollmentId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = enrollment.EnrollmentId });
    }

    public ActionResult Edit(int id)
    {
      Enrollment thisEnrollment = _db.Enrollments.FirstOrDefault(enrollments => enrollments.EnrollmentId == id);
      return View(thisEnrollment);
    }

    [HttpPost]
    public ActionResult Edit(Enrollment enrollment)
    {
      _db.Enrollments.Update(enrollment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Enrollment thisEnrollment = _db.Enrollments.FirstOrDefault(enrollments => enrollments.EnrollmentId == id);
      return View(thisEnrollment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Enrollment thisEnrollment = _db.Enrollments.FirstOrDefault(enrollments => enrollments.EnrollmentId == id);
      _db.Enrollments.Remove(thisEnrollment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      StudentEnrollment joinEntry = _db.StudentEnrollments.FirstOrDefault(entry => entry.StudentEnrollmentId == joinId);
      _db.StudentEnrollments.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}