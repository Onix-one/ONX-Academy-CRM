using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ISqlManagersRepository<Manager> _managerRepository;
      
        public ManagerService(ISqlManagersRepository<Manager> managerRepository)
        {
            _managerRepository = managerRepository;
        }
        public IEnumerable<Manager> GetAll()
        {
            return _managerRepository.GetAll();
        }

        public Task<IEnumerable<Manager>> GetAllAsync()
        {
            return _managerRepository.GetAllAsync();
        }

        public Task<Manager> GetEntityByIdAsync(int id)
        {
            return _managerRepository.GetEntityByIdAsync(id);
        }

        public void Create(Manager manager)
        {
            _managerRepository.Create(manager);
        }

        public void Update(Manager entity)
        {
            _managerRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _managerRepository.Delete(id);
        }

        public Task<bool> CheckIfManagerExists(string email)
        {
           return _managerRepository.CheckIfManagerExists(email);
        }
    }
}
