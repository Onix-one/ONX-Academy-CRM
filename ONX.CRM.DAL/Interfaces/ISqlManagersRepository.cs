using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlManagersRepository<T> : IRepository<T>
    {
        Task<bool> CheckIfManagerExists(string email);
    }
}
