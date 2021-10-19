using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly ISqlStudentsRepository<Student> _studentRepository;
        public StudentService(ISqlStudentsRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public IEnumerable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }
        public Task<IEnumerable<Student>> GetAllAsync()
        {
            return _studentRepository.GetAllAsync();
        }
        public Task<Student> GetEntityByIdAsync(int id)
        {
            return _studentRepository.GetEntityByIdAsync(id);
        }
        public void Create(Student item)
        {
            _studentRepository.Create(item);
        }
        public void Update(Student item)
        {
            _studentRepository.Update(item);
        }
        public void Delete(int id)
        {
            _studentRepository.Delete(id);
        }
        public async Task<IEnumerable<Student>> SearchStudents(string query, int courseId, int type)
        {
            if (!string.IsNullOrEmpty(query))
            {
               return await _studentRepository.GetStudentsByQuery(query);
            }
            if (courseId != 0)
            {
                return await _studentRepository.GetStudentsByCourseId(courseId);
            }
            if (type != 0)
            {
                return await _studentRepository.GetStudentsByType(type);
            }
            return null;
        }
        public async Task<Dictionary<int, string>> GetActiveCoursesIdTitle()
        {
            var courses = (await _studentRepository.GetAllAsync()).Select(r => r.Group.Course);
            var activeCoursesIdTitle = new Dictionary<int, string>();
           
            foreach (var course in courses)
            {
                activeCoursesIdTitle[course.Id] = course.Title;
            }
            return activeCoursesIdTitle;
        }
    }
}
