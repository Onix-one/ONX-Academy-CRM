using System.Collections.Generic;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentRequestService : IEntityService<StudentRequest>
    {
        void AssignRequestToGroups(IEnumerable<StudentRequest> requests, int groupId);
    }
}
