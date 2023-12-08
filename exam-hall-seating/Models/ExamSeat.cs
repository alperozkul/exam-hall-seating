using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class ExamSeat
    {
        [Key]
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam? Exam { get; set; }

        [ForeignKey("ClassroomDetail")]
        public int ClassroomDetailId { get; set; }
        public ClassroomDetail? ClassroomDetail { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        
    }
}
