using exam_hall_seating.Models;

namespace exam_hall_seating.Interfaces
{
    public interface IClassroomRepository
    {
        List<Classroom> GetAll();
        Task<Classroom> GetByIdAsync(int id);
        Classroom GetByName(string name);
        bool Add(Classroom classroom);
        bool Update(Classroom classroom);
        bool Delete(Classroom classroom);
        bool Save();
    }
}
