using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateOnly BirthDate { get; set; }

        // Add these properties
        public string BloodType { get; set; }
        public string Hall { get; set; }
        public string Department { get; set; }
    }
}
