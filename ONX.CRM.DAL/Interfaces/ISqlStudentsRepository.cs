using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlStudentsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetListOfStudentsByParameters(string query, int courseId, int type, int skip, int take);
        Task<int> GetNumberOfStudentsByParameters(string query, int courseId, int type);
        Task<IEnumerable<T>> GetStudentsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfStudents();
        Task<IEnumerable<Course>> GetActiveCourses();
        Task<bool> CheckIfStudentExists(string email);
        Task<IEnumerable<T>> FindByUserIdAsync(string userId);
        Task<IEnumerable<T>> GetStudentsByGroupId(int id);
    }
}
