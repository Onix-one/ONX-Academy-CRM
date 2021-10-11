using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IGroupService : IEntityService<Group>
    {
        Task<IEnumerable<Group>> GetGroupsByStatus(string status);
        Task<IEnumerable<Group>> GetGroupsByQuery(string query);
    }
}
