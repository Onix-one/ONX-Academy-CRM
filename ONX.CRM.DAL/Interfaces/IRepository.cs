using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface IRepository<T> 
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetEntityByIdAsync(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
