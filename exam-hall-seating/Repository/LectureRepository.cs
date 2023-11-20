using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Repository
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Lecture lecture)
        {
            _context.Add(lecture);
            return Save();
        }

        public bool Delete(Lecture lecture)
        {
            _context.Remove(lecture);
            return Save();
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            return await _context.Lectures.Include(i => i.Department).ToListAsync();
        }

        public async Task<Lecture> GetByIdAsync(int id)
        {
            return await _context.Lectures.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Lecture lecture)
        {
            _context.Update(lecture);
            return Save();
        }
    }
}
