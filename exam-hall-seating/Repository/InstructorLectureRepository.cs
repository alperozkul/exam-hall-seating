using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Repository
{
    public class InstructorLectureRepository : IInstructorLectureRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorLectureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(InstructorLecture instructorLecture)
        {
            _context.InstructorLectures.Add(instructorLecture);
            return Save();
        }

        public bool Delete(InstructorLecture instructorLecture)
        {
            _context.Remove(instructorLecture);
            return Save();
        }
        public async Task<IEnumerable<InstructorLecture>> GetAllAsync()
        {
            return await _context.InstructorLectures.ToListAsync();
        }

        public async Task<IEnumerable<InstructorLecture>> ShowInstructorLectures(string appUserId)
        {
            return await _context.InstructorLectures
                .Where(il => il.AppUserId == appUserId)
                .Include(il => il.Lecture)
                .Include(il => il.AppUser)
                .ToListAsync();
        }

        public async Task<InstructorLecture> GetByIdAsync(int id)
        {
            return await _context.InstructorLectures.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(InstructorLecture instructorLecture)
        {
            _context.Update(instructorLecture);
            return Save();
        }
    }
}
