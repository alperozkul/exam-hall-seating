using exam_hall_seating.Models;
using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.ViewModels.EnrollmentVM
{
    public class AssignmentViewModel
    {
        public int LectureId { get; set; }
        public int? Year { get; set; }
        public Lecture? Lectures { get; set; }

        public List<StudentViewModel>? Students { get; set; }
        public Lecture.Periods? Period { get; set; }
        public Enrollment.Grades? Grade { get; set; }
    }
}
