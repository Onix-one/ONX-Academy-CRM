using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.DAL.Interfaces;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Services
{
    public class LessonService : ILessonService
    {
        private readonly ISqlLessonsRepository<Lesson> _repository;
        public LessonService(ISqlLessonsRepository<Lesson> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Lesson> GetAll()
        {
            return _repository.GetAll();
        }
        public Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
        public Task<Lesson> GetEntityByIdAsync(int id)
        {
            return _repository.GetEntityByIdAsync(id);
        }
        public void Create(Lesson entity)
        { 
            _repository.Create(entity);
        }
        public void Update(Lesson entity)
        {
            _repository.Update(entity);
        }
        public void Delete(int id)
        { 
            _repository.Delete(id);
        }
        public Task<IEnumerable<Lesson>> GetLessonsByGroupId(int id)
        {
            return _repository.GetLessonsByGroupId(id);
        }
        public void SaveMaterial(int id, byte[] content)
        {
            var lesson = _repository.GetEntityByIdAsync(id).Result;
            lesson.Materials = content;
            _repository.Update(lesson);
        }
        public void DeleteMaterial(int id)
        {
            var lesson = _repository.GetEntityByIdAsync(id).Result;
            lesson.Materials = null;
            _repository.Update(lesson);
        }
    }
}

