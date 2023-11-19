using exam_hall_seating.Models;

namespace exam_hall_seating.ViewModels.ExamVM
{
    public class CreateExamViewModel
    {
        public int LectureId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
