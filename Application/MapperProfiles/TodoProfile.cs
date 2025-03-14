using AutoMapper;
using StudentManagement.Application.DTOs.CourseDTO;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.MapperProfiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            // Map Course to CourseViewModel
            CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate));

            // Map Course to CourseCreateModel destination
            CreateMap<CourseCreateModel, Course>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CourseName))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate)).ReverseMap();
            // Map CourseCreateModel to Course

            CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, config =>
                {
                    config.PreCondition(src => src.Name != null);
                    config.MapFrom(src => src.Name);
                });
        }
    }
}

