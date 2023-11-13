using Microsoft.AspNetCore.Mvc.Rendering;

namespace exam_hall_seating.ViewModels.StudentVM
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public int DepartmentId { get; set; }

    }
}
