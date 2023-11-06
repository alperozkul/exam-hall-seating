using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class InstructorLecture
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        public Lecture Lecture { get; set;}
    }
}
