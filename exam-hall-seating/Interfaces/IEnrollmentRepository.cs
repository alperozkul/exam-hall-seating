using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.EnrollmentVM;
using exam_hall_seating.ViewModels.ExamVM;
using Microsoft.AspNetCore.Components.Web;

namespace exam_hall_seating.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment> GetByLectureIdAsync(int id);
        Task<List<EnrolledStudentViewModel>> GetAllStudentsByLectureId(int lectureId);
        Task<List<StudentViewModel>> GetAllStudentByLectureAsync(int lectureId);
        Task EnrollStudentsAsync(int lectureId, List<StudentViewModel> students);
        bool Add(Enrollment enrollment);
        bool Save();

    }
}
