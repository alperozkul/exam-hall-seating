using exam_hall_seating.Models;

namespace exam_hall_seating.ViewModels.InstructorVM
{
    public class AttendInstructorViewModel
    {
        public int LectureId{ get; set; }
        public Lecture? Lectures { get; set; }
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
