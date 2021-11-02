using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlManagersRepository<T> : IRepository<T>
    {
        Task<bool> CheckIfManagerExists(string email);
        Task<IEnumerable<Manager>> FindByUserIdAsync(string userId);
    }
}
