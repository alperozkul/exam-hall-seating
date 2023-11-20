namespace exam_hall_seating.ViewModels.ExamVM
{
    public class EditExamViewModel
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
