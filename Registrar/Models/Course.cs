using System.Collections.Generic;

namespace Registrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public List<Students> Students { get; set; }
  }
}