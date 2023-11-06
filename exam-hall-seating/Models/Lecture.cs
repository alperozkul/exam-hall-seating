using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public Periods Period { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<InstructorLecture> InstructorLectures { get; set; }
        public ICollection<Exam> Exams { get; set; }

        public enum Periods
        {
            Fall = 1,
            Spring = 2
        }

    }
}
