using exam_hall_seating.Models;

namespace exam_hall_seating.Interfaces
{
    public interface ILectureRepository
    {
        Task<IEnumerable<Lecture>> GetAllAsync();
        Task<Lecture> GetByIdAsync(int id);
        bool Add(Lecture lecture);
        bool Update(Lecture lecture);
        bool Delete(Lecture lecture);
        bool Save();
    }
}
