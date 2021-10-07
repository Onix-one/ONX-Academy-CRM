using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class TeacherService : EntityService<Teacher>, ITeacherService
    {
        public TeacherService(IRepository<Teacher> repository) : base(repository) { }

        public async Task<IEnumerable<Teacher>> SearchTeachers(string query)
        {
            var teachersList = await _repository.GetAllAsync();
            var teachers = new List<Teacher>();
            foreach (var teacher in teachersList)
            {
                if (teacher.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    teachers.Add(teacher);
                    continue;
                }
                if (teacher.LastName.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    teachers.Add(teacher);
                    continue;
                }
                if (teacher.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    teachers.Add(teacher);
                    continue;
                }
                if (teacher.Phone.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    teachers.Add(teacher);
                    continue;
                }
                if (teacher.WorkExperience.Contains(query, StringComparison.OrdinalIgnoreCase))
                {
                    teachers.Add(teacher);
                }
            }
            return teachers;
        }
    }
}
