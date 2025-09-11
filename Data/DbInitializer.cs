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
        }
    }
}
