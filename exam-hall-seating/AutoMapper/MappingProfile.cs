using AutoMapper;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.ExamVM;
using exam_hall_seating.ViewModels.InstructorVM;
using exam_hall_seating.ViewModels.LectureVM;
using exam_hall_seating.ViewModels.StudentVM;

namespace exam_hall_seating.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, CreateStudentViewModel>().ReverseMap();
            CreateMap<Student, EditStudentViewModel>().ReverseMap();
            CreateMap<AppUser, CreateInstructorViewModel>().ReverseMap();
            CreateMap<AppUser, EditInstructorViewModel>().ReverseMap();
            CreateMap<Lecture, CreateLectureViewModel>().ReverseMap();
            CreateMap<Lecture, EditLectureViewModel>().ReverseMap();
            CreateMap<Exam, CreateExamViewModel>().ReverseMap();
            CreateMap<Exam, EditExamViewModel>().ReverseMap();

        }
    }
}
