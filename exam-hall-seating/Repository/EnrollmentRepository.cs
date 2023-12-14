using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.EnrollmentVM;
using exam_hall_seating.ViewModels.ExamVM;
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
            Lecture lecture = await _context.Lectures.FindAsync(lectureId);

            if (lecture == null)
            {
                return new List<StudentViewModel>();
            }

            int lecturePeriod = ((lecture.Year - 1) * 2) + (int)lecture.Period; //3. yıl 1. dönem dersi (3-1)*2 + 1 = 5. dönem
            int currentYear = DateTime.Now.Year;

            var studentsMatchingCondition = await _context.Students
                .Where(student =>
                    (student.Period == lecturePeriod &&
                        !student.Enrollments.Any(enrollment =>
                        enrollment.StudentId == student.Id && enrollment.LectureId == lectureId)) ||
                    (student.Enrollments.Any(enrollment =>
                        enrollment.StudentId == student.Id && enrollment.LectureId == lectureId &&
                        enrollment.Grade == Enrollment.Grades.F &&
                        !student.Enrollments.Any(enrollment =>
                            enrollment.StudentId == student.Id && enrollment.LectureId == lectureId &&
                            enrollment.Grade == null))))
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


        public async Task EnrollStudentsAsync(int lectureId, List<StudentViewModel> students)
        {

            //Lecture.Period bilgisini Enrollment.Period bilgisine çeviriyoruz.
            Enrollment.Periods lecturePeriod = (Enrollment.Periods)_context.Lectures.FirstOrDefault(s => s.Id == lectureId).Period;

            foreach (var studentViewModel in students)
            {
                var enrollment = new Enrollment
                {
                    StudentId = _context.Students.FirstOrDefault(s => s.Number == studentViewModel.Number).Id,
                    LectureId = lectureId,
                    Year = DateTime.Now.Year,
                    Period = lecturePeriod,
                    Grade = null
                };
                _context.Enrollments.Add(enrollment);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<EnrolledStudentViewModel>> GetAllStudentsByLectureId(int lectureId)
        {
            List<EnrolledStudentViewModel> enrolledStudents = await _context.Enrollments.Where(x => x.LectureId == lectureId && x.Grade == null)
               .Select(x => new EnrolledStudentViewModel
               {
                   Id = x.Student.Id,
                   Number = x.Student.Number,
                   FirstName = x.Student.FirstName,
                   LastName = x.Student.LastName,
                   Mail = x.Student.Mail          
               }).ToListAsync();
            return enrolledStudents;
        }
    }
}
