using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlGroupsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetGroupsByStatus(int status);
        Task<IEnumerable<T>> GetGroupsByQuery(string query);
    }
}
