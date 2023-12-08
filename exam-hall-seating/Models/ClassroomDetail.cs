using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class ClassroomDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Classroom")]
        public int ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
        public int BlockNo { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string? ValidColumns { get; set; }
        public int Capacity => Row * Column;

        public ICollection<ExamSeat>? ExamSeats { get; set; }
    }
}
