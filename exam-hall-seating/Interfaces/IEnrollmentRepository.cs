using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.EnrollmentVM;
using Microsoft.AspNetCore.Components.Web;

namespace exam_hall_seating.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment> GetByLectureIdAsync(int id);
        Task<List<StudentViewModel>> GetAllStudentByLectureAsync(int lectureId);       
        bool Add(Enrollment enrollment);
        bool Save();

    }
}
