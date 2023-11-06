using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam_hall_seating.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }



        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<ExamSeat> ExamSeats { get; set; }
    }
}
