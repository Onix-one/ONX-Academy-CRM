using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentService : IEntityService<Student>
    {
        Task<IEnumerable<Student>> SearchStudents(string query, int courseId, int type);
        Task<Dictionary<int, string>> GetActiveCoursesIdTitle();
    }
}
