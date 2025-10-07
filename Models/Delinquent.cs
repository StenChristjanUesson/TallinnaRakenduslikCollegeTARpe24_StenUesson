using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikCollegeTARpe24_StenUesson.Models
{
    public class Delinquent
    {
        [Key]
        public int DelinquentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Violtaions violtaions { get; set; }
        public TeacherOrStudent teacherOrStudent { get; set; }
        public string? ViolationDescription { get; set; }

        public enum Violtaions
        {
            Truancy, KoduvalveRikkumised, AlaealisteTarvitamine, Põgenemine, Kontrollimatus, VaraKuriteod, Narkokuriteod, RünnakJaPõrutus, SissetungJaRöövimine, Vandalism
        }

        public enum TeacherOrStudent 
        { 
            Õpilane, Õpetaja
        }
    }
}
