using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentRequestService : IEntityService<StudentRequest>
    {
        void AssignRequestToGroups(IEnumerable<StudentRequest> requests, int groupId);
        Task<Dictionary<int, string>> GetActiveCoursesIdTitle();
        Task<IEnumerable<StudentRequest>> GetRequestsByCourseId(int courseId, int skip, int take);
        Task<IEnumerable<StudentRequest>> GetRequestsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfRequests();
        Task<int> GetNumberOfRequestsByCourseId(int courseId);
    }
}
