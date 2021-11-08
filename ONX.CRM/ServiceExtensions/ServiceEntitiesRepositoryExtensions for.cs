using Microsoft.Extensions.DependencyInjection;
using ONX.CRM.DAL.Dapper.Repositories;
using ONX.CRM.DAL.EF.Repositories;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.ServiceExtensions
{
    public static class ServiceEntitiesRepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISqlStudentsRepository<Student>, SqlStudentsRepository>();
            services.AddScoped<ISqlGroupsRepository<Group>, SqlGroupsRepository>();
            services.AddScoped<ISqlTeachersRepository<Teacher>, SqlTeachersRepository>();
            services.AddScoped<IRepository<Course>, SqlCoursesRepository>();
            services.AddScoped<ISqlManagersRepository<Manager>, SqlManagersRepository>();
            services.AddScoped<IRepository<Specialization>, SqlSpecializationsRepository>();
            services.AddScoped<ISqlStudentRequestsRepository<StudentRequest>, SqlStudentRequestsRepository>();
            services.AddScoped<ISqlLessonsRepository<Lesson>, SQLLessonsRepository>();
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
