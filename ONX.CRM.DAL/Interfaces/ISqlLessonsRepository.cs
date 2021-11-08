using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONX.CRM.DAL.Interfaces
{
    public interface ISqlLessonsRepository<T> : IRepository<T>
    {
        Task<IEnumerable<T>> GetLessonsByGroupId(int id);
    }
}
