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

        public Task<IEnumerable<Student>> GetStudentsWithSkipAndTakeAsync(int skip, int take)
        {
            return _studentRepository.GetStudentsWithSkipAndTakeAsync(skip, take);
        }

        public Task<int> GetNumberOfStudents()
        {
            return _studentRepository.GetNumberOfStudents();
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


        public Task<IEnumerable<Student>> GetListOfStudentsByParameters(string query, int courseId, int type, int skip,
            int take)
        {
            return _studentRepository.GetListOfStudentsByParameters(query, courseId, type, skip, take);
        }

        public Task<int> GetNumberOfStudentsByParameters(string query, int courseId, int type)
        {
            return _studentRepository.GetNumberOfStudentsByParameters(query, courseId, type);
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

