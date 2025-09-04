using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TallinnaRakenduslikCollegeTARpe24_StenUesson.Models;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuider) 
        {
            modelBuider.Entity<Course>().ToTable("Course");
            modelBuider.Entity<Student>().ToTable("Student");
            modelBuider.Entity<Enrollment>().ToTable("Enrollment");
        }
    }
}
