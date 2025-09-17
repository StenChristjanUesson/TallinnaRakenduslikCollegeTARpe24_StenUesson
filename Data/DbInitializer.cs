using TallinnaRakenduslikCollegeTARpe24_StenUesson.Models;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student
                {
                    FirstName = "Sten",
                    LastName = "Uesson",
                    EnrollmentDate = DateTime.Now,
                }
            };

            if (context.Instructors.Any()) { return; }
            var instructors = new Instructor[]
            {
                new Instructor
                {
                    LastName = "AAA",
                    FirstName = "EE",
                    HireDate = DateTime.Parse("2727-03-12")
                },
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();
        }
    }
}
