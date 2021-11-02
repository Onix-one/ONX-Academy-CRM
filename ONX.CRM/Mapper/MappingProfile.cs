using AutoMapper;
using ONX.CRM.DAL.Models;
using ONX.CRM.ViewModel;

namespace ONX.CRM.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<Person, PersonViewModel>().ReverseMap();
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();
            CreateMap<Manager, ManagerViewModel>().ReverseMap();
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<Specialization, SpecializationViewModel>().ReverseMap();
            CreateMap<StudentRequest, StudentRequestViewModel>().ReverseMap();
            CreateMap<Manager, ProfileViewModel>().ReverseMap();
            CreateMap<Student, ProfileViewModel>().ReverseMap();
            CreateMap<Group, GroupViewModel>()
                .ForMember(model => model.TeacherName, 
                    map => map
                        .MapFrom(g => $"{g.Teacher.LastName} {g.Teacher.FirstName}"))
                .ReverseMap();
        }
    }
}
