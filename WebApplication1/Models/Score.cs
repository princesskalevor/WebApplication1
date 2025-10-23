using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public decimal Mark { get; set; }
        public Char Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
