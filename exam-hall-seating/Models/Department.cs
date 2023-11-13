using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<AppUser> AppUsers{ get; set; }
        public ICollection<Lecture> Lectures { get; set; }

    }
}
