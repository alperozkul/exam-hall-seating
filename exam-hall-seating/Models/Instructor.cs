using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.Models
{
    public class Instructor 
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
