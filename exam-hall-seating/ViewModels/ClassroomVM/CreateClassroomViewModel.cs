using exam_hall_seating.Models;

namespace exam_hall_seating.ViewModels.ClassroomVM
{
    public class CreateClassroomViewModel
    {
        public Classroom? ClassroomData { get; set; }
        public List<ClassroomDetail> ClassroomDetail { get; set; }
        public int BlockNumber { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string? ValidColumns { get; set; }

        public CreateClassroomViewModel()
        {
            ClassroomDetail = new List<ClassroomDetail>();
            ClassroomData = new Classroom();
        }
    }
}
