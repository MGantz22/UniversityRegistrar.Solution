using System.Collections.Generic;

namespace Registrar.Models
{
  public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string Title { get; set; }
        public List<StudentEnrollment> JoinEntities { get;}
    }
}