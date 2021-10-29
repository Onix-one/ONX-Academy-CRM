using AutoMapper;
using ONX.CRM.Angular.Dto;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.Angular.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(model => model.Specialization,
                    map => map
                        .MapFrom(_ => _.Specialization.Title))
                .ReverseMap();
        }
    }
}
