using exam_hall_seating.Models;

namespace exam_hall_seating.Interfaces
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam> GetByIdAsync(int id);
        bool Add(Exam exam);
        bool Update(Exam exam);
        bool Delete(Exam exam);
        bool Save();
    }
}
