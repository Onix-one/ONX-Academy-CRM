using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IStudentService : IEntityService<Student>
    {
        Task<IEnumerable<Student>> GetListOfStudentsByParameters(string query, int courseId, int type, int skip, int take);
        Task<int> GetNumberOfStudentsByParameters(string query, int courseId, int type);
        Task<Dictionary<int, string>> GetActiveCoursesIdTitle();
        Task<IEnumerable<Student>> GetStudentsWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfStudents();
        Task<bool> CheckIfManagerExists(string email);
        Task<IEnumerable<Student>> FindByUserIdAsync(string userId);
        void SaveImage(int id, byte[] content);
        void DeleteImage(int id);
        Task<IEnumerable<Student>> GetStudentsByGroupId(int id);
    }
}
