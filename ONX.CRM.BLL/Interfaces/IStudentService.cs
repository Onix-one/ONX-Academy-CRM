using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentService : IEntityService<Student>
    {
        Task<IEnumerable<Student>> GetListOfStudentsByParameters(string query, int courseId, int type, int skip, int take);
        Task<int> GetNumberOfStudentsByParameters(string query, int courseId, int type);
        Task<Dictionary<int, string>> GetActiveCoursesIdTitle();
        Task<IEnumerable<Student>> GetStudentsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfStudents();
    }
}
