using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Lecture")]
        public int LectureId { get; set;}
        public Lecture? Lecture { get; set;}
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<ExamSeat>? ExamSeats { get; set; }
    }
}
