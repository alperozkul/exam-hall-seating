using exam_hall_seating.Models;

namespace exam_hall_seating.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        bool Add(Department department);
        bool Update(Department department);
        bool Delete(Department department);
        bool Save();
    }
}
