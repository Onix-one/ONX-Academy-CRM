using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlStudentRequestsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetRequestsByCourseId(int courseId);
    }
}
