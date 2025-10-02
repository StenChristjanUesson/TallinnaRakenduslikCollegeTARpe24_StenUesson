using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Models
{
    public class Delinquent
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Violtaions violtaions { get; set; }
        public TeacherOrStudent teacherOrStudent { get; set; }
        public string? ViolationDescription { get; set; }

        public enum Violtaions
        {

        }

        public enum TeacherOrStudent 
        { 
            
        }
    }
}
