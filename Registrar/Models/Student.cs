using System.Collections.Generic;

namespace Registrar.Models
{
  public class Student
  {
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public List<CourseStudent> JoinEntities { get;}
  }
}