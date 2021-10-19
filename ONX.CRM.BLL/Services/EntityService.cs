using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class EntityService<T> : IEntityService<T> where T: class
    {
        protected readonly IRepository<T> _repository;

        protected EntityService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
        public T GetEntityById(int id)
        {
            return _repository.GetEntity(id);
        }
        public void Create(T item)
        {
            _repository.Create(item);
        }
        public void Update(T item)
        {
            _repository.Update(item);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
