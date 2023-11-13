using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.Models
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; } 
        public string ClassName { get; set; }
        public int Floor { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Capacity { get; set; }

        public ICollection<ExamSeat>? ExamSeats { get; set;}
    }
}
