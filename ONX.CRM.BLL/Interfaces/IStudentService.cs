﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentService : IEntityService<Student>
    {
        Task<IEnumerable<Student>> SearchStudents(string query, int courseId, string type);
        Task<Dictionary<int, string>> GetCoursesForDropdown();
    }
}
