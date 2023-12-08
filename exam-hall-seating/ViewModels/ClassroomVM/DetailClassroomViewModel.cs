using exam_hall_seating.Models;

namespace exam_hall_seating.ViewModels.ClassroomVM
{
    public class DetailClassroomViewModel
    {
        public Classroom? Classroom { get; set; }
        public List<ClassroomDetail> ClassroomDetail { get; set; }

        public DetailClassroomViewModel()
        {
            ClassroomDetail = new List<ClassroomDetail>();
        }


    }
}
