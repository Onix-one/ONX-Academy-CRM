using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlStudentRequestsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetRequestsByCourseId(int courseId, int skip, int take);
        Task<IEnumerable<T>> GetRequestsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfRequests();
        Task<int> GetNumberOfRequestsByCourseId(int courseId);
    }
}
