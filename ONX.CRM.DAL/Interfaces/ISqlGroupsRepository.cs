using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlGroupsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetListOfGroupsByParameters(string query, int status, int skip, int take);
        Task<int> GetNumberOfGroupsByParameters(string query, int status);
        Task<IEnumerable<T>> GetGroupsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfGroups();
    }
}
