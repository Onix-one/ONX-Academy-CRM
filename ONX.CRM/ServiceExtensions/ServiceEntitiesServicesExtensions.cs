using Microsoft.Extensions.DependencyInjection;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.BLL.Services;

namespace ONX.CRM.ServiceExtensions
{
    public static class ServiceEntitiesServicesExtensions
    {
        public static IServiceCollection AddEntitiesServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IEntityService<Course>, EntityService<Course>>();
            services.AddScoped<IEntityService<Specialization>, EntityService<Specialization>>();
            services.AddScoped<IStudentRequestService, StudentRequestService>();
            return services;
        }
    }
}
