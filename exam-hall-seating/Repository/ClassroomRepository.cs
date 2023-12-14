using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassroomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Classroom classroom)
        {
            _context.Add(classroom);
            return Save();
        }

        public bool Delete(Classroom classroom)
        {
            _context.Remove(classroom);
            return Save();
        }

        public async Task<List<Classroom>> GetAllAsync()
        {
            return await _context.Classrooms.OrderBy(i => i.Floor).ToListAsync();
        }

        public async Task<Classroom> GetByIdAsync(int id)
        {
            return await _context.Classrooms.FirstOrDefaultAsync(classroom => classroom.Id == id);
        }

        public Classroom GetByName(string name)
        {
            return _context.Classrooms.FirstOrDefault(c => c.ClassName == name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Classroom classroom)
        {
            _context.Update(classroom);
            return Save();
        }
    }
}
