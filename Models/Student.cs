using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Models
{

    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public string Gender { get; set; }
        public int? age { get; set; }
        public string? Main_Language { get; set; }
    }
}
