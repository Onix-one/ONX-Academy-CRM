using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class EntityService<T> : IEntityService<T> where T : class
    {
        private readonly IRepository<T> _repository;
        protected EntityService(IRepository<T> repository)
        {
            _repository = repository;
        }
        public IEnumerable<T> GetAll() => _repository.GetAll();
        public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();
        public Task<T> GetEntityByIdAsync(int id) => _repository.GetEntityByIdAsync(id);
        public void Create(T item) => _repository.Create(item);
        public void Update(T item) => _repository.Update(item);
        public void Delete(int id) => _repository.Delete(id);

    }
}
