using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class CourseService : EntityService<Course>, ICourseService
    {
        public CourseService(IRepository<Course> repository) : base(repository)
        {
          
        }
    }
}
