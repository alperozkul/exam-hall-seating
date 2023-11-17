using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.EnrollmentVM;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            return Save();
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments.ToListAsync();
        }

        public async Task<List<StudentViewModel>> GetAllStudentByLectureAsync(int lectureId)
        {
            var lecture = await _context.Lectures.FindAsync(lectureId);

            if (lecture == null)
            {
                return new List<StudentViewModel>();
            }

            var studentsMatchingCondition = await _context.Students
                .Where(student => student.Period == ((lecture.Year - 1) * 2) + (int)lecture.Period 
                && !student.Enrollments.Any(enrollment => enrollment.StudentId == student.Id && enrollment.LectureId == lectureId)
                || student.Enrollments.Any(enrollment => enrollment.Grade == Enrollment.Grades.F && enrollment.LectureId == lectureId))
                .Select(student => new StudentViewModel
                {
                    Number = student.Number,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Year = student.Year,
                    Period = student.Period
                })
                .ToListAsync();

            return studentsMatchingCondition;
        }

        public Task<Enrollment> GetByLectureIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
