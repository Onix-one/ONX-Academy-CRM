using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.BLL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface ITeacherService : IEntityService<Teacher>
    {
        Task<IEnumerable<Teacher>> GetTeachersByQuery(string query);
    }
}
