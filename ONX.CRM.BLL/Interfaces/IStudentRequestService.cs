using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentRequestService : IEntityService<StudentRequest>
    {
        void AssignRequestToGroups(IEnumerable<StudentRequest> requests, int groupId);

        Task<IEnumerable<StudentRequest>> GetRequestsByCourseId(int courseId);

        Task<Dictionary<int, string>> GetCoursesForDropdown();
    }
}
