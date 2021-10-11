using Microsoft.Extensions.DependencyInjection;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Dapper.Repositories;
using ONX.CRM.DAL.EF.Repositories;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.ServiceExtensions
{
    public static class ServiceEntitiesRepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Student>, SqlStudentsRepository>();
            services.AddScoped<IRepository<Group>, SqlGroupsRepository>();
            services.AddScoped<IRepository<Teacher>, SqlTeachersRepository>();
            services.AddScoped<IRepository<Course>, SqlCoursesRepository>();
            services.AddScoped<IRepository<Specialization>, SqlSpecializationsRepository>();
            services.AddScoped<IRepository<StudentRequest>, SqlStudentRequestsRepository>();
            services.AddDapperRepositories();
            return services;
        }
        private static IServiceCollection AddDapperRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Course>, DapperCoursesRepository>();
            return services;
        }
    }
}
