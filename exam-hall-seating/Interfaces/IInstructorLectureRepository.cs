using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.EnrollmentVM;

namespace exam_hall_seating.Interfaces
{
    public interface IInstructorLectureRepository
    {
        Task<IEnumerable<InstructorLecture>> GetAllAsync();
        Task<InstructorLecture> GetByIdAsync(int id);
        Task<IEnumerable<InstructorLecture>> ShowInstructorLectures(string appUserId);
        bool Add(InstructorLecture instructorLecture);
        bool Update(InstructorLecture instructorLecture);
        bool Delete(InstructorLecture instructorLecture);
        bool Save();
    }
}
