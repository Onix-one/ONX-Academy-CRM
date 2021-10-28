using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IGroupService : IEntityService<Group>
    {
        Task<IEnumerable<Group>> GetListOfGroupsByParameters(string query, int status, int skip, int take);
        Task<int> GetNumberOfGroupsByParameters(string query, int status);
        Task<IEnumerable<Group>> GetGroupsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfGroups();
    }
}
