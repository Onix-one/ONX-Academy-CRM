using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ONX.CRM.Mapper;

namespace ONX.CRM.ServiceExtensions
{
    public static class ServiceMapperExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
