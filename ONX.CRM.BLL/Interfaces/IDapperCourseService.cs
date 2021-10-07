using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IDapperCourseService 
    {
        IEnumerable<Course> GetAll();
        Task<IEnumerable<Course>> GetAllAsync();
        Course GetCourse(int id);
        void Create(Course item);
        void Update(Course item);
        void Delete(int id);
    }
}
