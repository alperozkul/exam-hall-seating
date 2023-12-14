using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Exam exam)
        {
            _context.Exams.Add(exam);
            return Save();
        }

        public bool Delete(Exam exam)
        {
            _context.Remove(exam);
            return Save();
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            return await _context.Exams
                .Include(i => i.Lecture)
                .OrderByDescending(i => i.Date)
                .ToListAsync();
        }

        public async Task<Exam> GetByIdAsync(int id)
        {
            return await _context.Exams.Include(i => i.Lecture).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Exam exam)
        {
            _context.Update(exam);
            return Save();
        }
    }
}
