namespace Registrar.Models
{
  public class StudentEnrollment
  {
    public int StudentEnrollmentId  { get; set; }
    public int StudentId { get; set; }
    public int EnrollmentId { get; set; }
    public Student Student { get; set; }
    public Enrollment Enrollment { get; set; }
  }
}