using Microsoft.EntityFrameworkCore;

namespace Registrar.Models
{
  public class RegistrarContext : DbContext
  {
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public DbSet<StudentEnrollment> StudentEnrollments { get; set; }

    public RegistrarContext(DbContextOptions options) : base(options) { }
  }
}
