using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlTeachersRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetTeachersByQuery(string query, int skip, int take);
        Task<int> GetNumberOfTeachersByQuery(string query);
        Task<IEnumerable<T>> GetTeachersWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfTeachers();
        Task<bool> CheckIfTeacherExists(string email);
        Task<IEnumerable<T>> FindByUserIdAsync(string userId);
    }
}
