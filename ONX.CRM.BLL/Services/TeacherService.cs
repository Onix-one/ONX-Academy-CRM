using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

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
        public Task<Teacher> GetEntityByIdAsync(int id)
        {
            return _teachersRepository.GetEntityByIdAsync(id);
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
        public Task<IEnumerable<Teacher>> GetTeachersByQuery(string query, int skip, int take)
        {
            return  _teachersRepository.GetTeachersByQuery(query, skip, take);
        }
        public Task<int> GetNumberOfTeachersByQuery(string query)
        {
            return _teachersRepository.GetNumberOfTeachersByQuery(query);
        }
        public Task<IEnumerable<Teacher>> GetTeachersWithSkipAndTakeAsync(int skip, int take)
        {
            return _teachersRepository.GetTeachersWithSkipAndTakeAsync(skip, take);
        }
        public Task<int> GetNumberOfTeachers()
        {
            return _teachersRepository.GetNumberOfTeachers();
        }
        public Task<bool> CheckIfManagerExists(string email)
        {
            return _teachersRepository.CheckIfTeacherExists(email);
        }
        public Task<IEnumerable<Teacher>> FindByUserIdAsync(string userId)
        {
            return _teachersRepository.FindByUserIdAsync(userId);
        }
        public void SaveImage(int id, byte[] content)
        {
            var teacher = _teachersRepository.GetEntityByIdAsync(id).Result;

            teacher.Image = content;

            _teachersRepository.Update(teacher);
        }
        public void DeleteImage(int id)
        {
            var teacher = _teachersRepository.GetEntityByIdAsync(id).Result;

            teacher.Image = null;

            _teachersRepository.Update(teacher);
        }
    }
}
