using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Enums;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentService : EntityService<Student>, IStudentService
    {
        public StudentService(IRepository<Student> repository) : base(repository) { }
       
        public async Task<IEnumerable<Student>> SearchStudents(string query, int courseId, StudentType type)
        {
            if (!string.IsNullOrEmpty(query))
            {
               return await GetStudentsByQuery(query);
            }

            if (courseId != 0)
            {
                return await GetStudentsByCourseId(courseId);
            }

            if (!string.IsNullOrEmpty(type.ToString()))
            {
                return await GetStudentsByType(type);
            }

            return null;
        }

        private async Task<IEnumerable<Student>> GetStudentsByType(StudentType type)
        {
            var studentsList = (await _repository.GetAllAsync()).Where(s => s.Type.ToString() == type.ToString());
            return studentsList;
        }

        private async Task<IEnumerable<Student>> GetStudentsByCourseId(int courseId)
        {
            var studentsList = (await _repository.GetAllAsync()).Where(s=>s.Group.CourseId == courseId);
            return studentsList;


        }

        private async Task<IEnumerable<Student>> GetStudentsByQuery(string query)
        {
            var allStudents = await _repository.GetAllAsync();

            return allStudents.Where(s => s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || s.Email.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || s.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)
                                             || s.Group.Number.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Dictionary<int, string>> GetActiveCoursesIdTitle()
        {
            var courses = (await _repository.GetAllAsync()).Select(r => r.Group.Course);
            var activeCoursesIdTitle = new Dictionary<int, string>();
           
            foreach (var course in courses)
            {
                activeCoursesIdTitle[course.Id] = course.Title;
            }
            return activeCoursesIdTitle;
        }
    }
}
