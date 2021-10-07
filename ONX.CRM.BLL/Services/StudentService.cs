using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Interfaces;
using ONX.CRM.BLL.Models;
using ONX.CRM.DAL.Interfaces;

namespace ONX.CRM.BLL.Services
{
    public class StudentService : EntityService<Student>, IStudentService
    {
        public StudentService(IRepository<Student> repository) : base(repository) { }
       
        public async Task<IEnumerable<Student>> SearchStudents(string query)
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
    }
}
