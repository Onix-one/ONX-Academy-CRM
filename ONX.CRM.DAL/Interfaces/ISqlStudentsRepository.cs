using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlStudentsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetStudentsByType(int type);
        Task<IEnumerable<T>> GetStudentsByCourseId(int courseId);
        Task<IEnumerable<T>> GetStudentsByQuery(string query);
    }
}
