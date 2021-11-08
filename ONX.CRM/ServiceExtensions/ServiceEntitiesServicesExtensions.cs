using Microsoft.Extensions.DependencyInjection;
using ONX.CRM.BLL.Interfaces;
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
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ISpecializationService, SpecializationService>();
            services.AddScoped<IStudentRequestService, StudentRequestService>();
            services.AddScoped<ILessonService, LessonService>();

            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
