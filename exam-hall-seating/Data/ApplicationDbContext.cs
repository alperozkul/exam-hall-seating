using exam_hall_seating.Models;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamSeat> ExamSeats { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorLecture> InstructorLectures { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
