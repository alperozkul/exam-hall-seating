using AutoMapper;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels;

namespace exam_hall_seating.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, CreateStudentViewModel>().ReverseMap();
            CreateMap<Student, EditStudentViewModel>().ReverseMap();
            
        }
    }
}
