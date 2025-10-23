using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
    }
}
