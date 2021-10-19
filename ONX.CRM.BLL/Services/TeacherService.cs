using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class TeacherService :  ITeacherService
    {
        private readonly ISqlTeachersRepository<Teacher> _teachersRepository;

        public TeacherService(ISqlTeachersRepository<Teacher> teachersRepository)
        {
            _teachersRepository = teachersRepository;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return _teachersRepository.GetAll();
        }
        public Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return _teachersRepository.GetAllAsync();
        }
        public Teacher GetEntityById(int id)
        {
            return _teachersRepository.GetEntity(id);
        }
        public void Create(Teacher item)
        {
            _teachersRepository.Create(item);
        }
        public void Update(Teacher item)
        {
            _teachersRepository.Update(item);
        }
        public void Delete(int id)
        {
            _teachersRepository.Delete(id);
        }
        public async Task<IEnumerable<Teacher>> GetTeachersByQuery(string query)
        {
            return await _teachersRepository.GetTeachersByQuery(query);
        }
    }
}
