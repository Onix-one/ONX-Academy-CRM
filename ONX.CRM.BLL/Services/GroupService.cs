using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

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
        public Task<IEnumerable<Group>> GetListOfGroupsByParameters(string query, int status, int skip, int take)
        {
            return _repository.GetListOfGroupsByParameters(query, status, skip, take);
        }
        public Task<int> GetNumberOfGroupsByParameters(string query, int status)
        {
            return _repository.GetNumberOfGroupsByParameters(query, status);
        }
        public Task<IEnumerable<Group>> GetGroupsWithSkipAndTakeAsync(int skip, int take)
        {
            return _repository.GetGroupsWithSkipAndTakeAsync(skip, take);
        }
        public Task<int> GetNumberOfGroups()
        {
            return _repository.GetNumberOfGroups();
        }
        public Task<IEnumerable<Group>> GetGroupsByTeacherId(int id)
        {
            return _repository.GetGroupsByTeacherId(id);
        }
    }
}
