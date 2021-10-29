using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Services
{
    public class CourseService : EntityService<Course>, ICourseService
    {
        public CourseService(IRepository<Course> repository) : base(repository) { }
    }
}
