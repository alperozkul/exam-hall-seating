using exam_hall_seating.Models;

namespace exam_hall_seating.ViewModels.ExamVM
{
    public class ArrangementViewModel
    {
        public string LectureName { get; set; }
        public int LectureId { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<int> SelectedClassrooms { get; set; }
        public List<EnrolledStudentViewModel>? Students { get; set; }
        public string? SelectedAlgorithm { get; set; }
    }
}
