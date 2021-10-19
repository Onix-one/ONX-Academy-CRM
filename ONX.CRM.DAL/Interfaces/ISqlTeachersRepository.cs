using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlTeachersRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetTeachersByQuery(string query);
    }
}
