using AutoMapper;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.InstructorVM;
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

            
        }
    }
}
