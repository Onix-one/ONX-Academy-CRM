using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentService : EntityService<Student>, IStudentService
    {
        public StudentService(IRepository<Student> repository) : base(repository) { }
       
        public async Task<IEnumerable<Student>> SearchStudents(string query, int courseId, string type)
        {
            if (!string.IsNullOrEmpty(query))
            {
               return await GetStudentsByQuery(query);
            }

            if (courseId != 0)
            {
                return await GetStudentsByCourseId(courseId);
            }

            if (!string.IsNullOrEmpty(type))
            {
                return await GetStudentsByType(type);
            }

            return null;
        }

        private async Task<IEnumerable<Student>> GetStudentsByType(string type)
        {
            var studentsList = (await _repository.GetAllAsync()).Where(s => s.Type.ToString() == type);
            return studentsList;
        }

        private async Task<IEnumerable<Student>> GetStudentsByCourseId(int courseId)
        {
            var studentsList = (await _repository.GetAllAsync()).Where(s=>s.Group.CourseId == courseId);
            return studentsList;


        }

        private async Task<IEnumerable<Student>> GetStudentsByQuery(string query)
        {
            var studentsList = await _repository.GetAllAsync();
            var students = new List<Student>();
            foreach (var student in studentsList)
            {
                if (student.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    students.Add(student);
                    continue;
                }
                if (student.LastName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    students.Add(student);
                    continue;
                }
                if (student.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    students.Add(student);
                    continue;
                }
                if (student.Phone.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    students.Add(student);
                    continue;
                }
                if (student.Group.Number.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    students.Add(student);
                }
            }
            return students;
        }


        public async Task<Dictionary<int, string>> GetCoursesForDropdown()
        {
            var courses = (await _repository.GetAllAsync()).Select(r => r.Group.Course);
            var coursesForDropMenu = new Dictionary<int, string>();
           
            foreach (var course in courses)
            {
                coursesForDropMenu[course.Id] = course.Title;
            }
            return coursesForDropMenu;
        }

    }
}
