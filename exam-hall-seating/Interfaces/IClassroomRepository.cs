using exam_hall_seating.Models;

namespace exam_hall_seating.Interfaces
{
    public interface IClassroomRepository
    {
        Task<IEnumerable<Classroom>> GetAllAsync();
        Task<Classroom> GetByIdAsync(int id);
        int GetIdByName(string name);
        bool Add(Classroom classroom);
        bool Update(Classroom classroom);
        bool Delete(Classroom classroom);
        bool Save();
    }
}
