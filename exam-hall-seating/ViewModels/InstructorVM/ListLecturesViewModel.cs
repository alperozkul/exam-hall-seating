using exam_hall_seating.Models;

namespace exam_hall_seating.ViewModels.InstructorVM
{
    public class ListLecturesViewModel
    {
        public int InstructorLectureId { get; set; }
        public string AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AppUser? AppUser { get; set; }
        public int LectureId { get; set; }
        public Lecture? Lecture { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Lecture.Periods Period { get; set; }

    }
}
