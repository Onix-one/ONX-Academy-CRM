using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly ISqlGroupsRepository<Group> _repository;
        public GroupService(ISqlGroupsRepository<Group> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Group> GetAll()
        {
            return _repository.GetAll();
        }
        public Task<IEnumerable<Group>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
        public Task<Group> GetEntityByIdAsync(int id)
        {
            return _repository.GetEntityByIdAsync(id);
        }
        public void Create(Group item)
        {
            _repository.Create(item);
        }
        public void Update(Group item)
        {
            _repository.Update(item);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public async Task<IEnumerable<Group>> GetGroupsByStatus(int status)
        {
            return await _repository.GetGroupsByStatus(status);
        }
        public async Task<IEnumerable<Group>> GetGroupsByQuery(string query)
        {
            return await _repository.GetGroupsByQuery(query);
        }
    }
}
