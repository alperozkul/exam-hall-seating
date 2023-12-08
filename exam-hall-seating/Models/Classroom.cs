using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.Models
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; } 
        public string ClassName { get; set; }
        public int Floor { get; set; }
        public byte[]? ImageData { get; set; }

        public ICollection<ClassroomDetail>? ClassroomDetails { get; set; }
        
    }
}
