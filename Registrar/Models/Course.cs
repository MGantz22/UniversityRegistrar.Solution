using System.Collections.Generic;

namespace Registrar.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
    public List<CourseStudent> JoinEntities { get; }


  }
}