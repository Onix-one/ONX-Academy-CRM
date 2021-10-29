using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface ITeacherService : IEntityService<Teacher>
    {
        Task<IEnumerable<Teacher>> GetTeachersByQuery(string query, int skip, int take);
        Task<int> GetNumberOfTeachersByQuery(string query);
        Task<IEnumerable<Teacher>> GetTeachersWithSkipAndTakeAsync(int skip, int take);
        Task<int> GetNumberOfTeachers();
    }
}
