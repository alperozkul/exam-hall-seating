using exam_hall_seating.Models;

namespace exam_hall_seating.Interfaces
{
    public interface IClassroomDetailRepository
    {
        Task<IEnumerable<ClassroomDetail>> GetAllAsync();
        Task<ClassroomDetail> GetByIdAsync(int id);
        List<ClassroomDetail> GetAllDetailById(int id);
        Task CreateClassroomBlocksAsync(List<ClassroomDetail> classroomDetail);
        Task<ClassroomDetail> AddBlock(ClassroomDetail block);
        bool Add(ClassroomDetail classroomDetail);
        bool Update(ClassroomDetail classroomDetail);
        bool Delete(ClassroomDetail classroomDetail);
        bool Save();
    }
}
