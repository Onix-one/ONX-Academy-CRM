using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IEntityService<T>
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetEntityByIdAsync(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
