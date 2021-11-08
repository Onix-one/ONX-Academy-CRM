using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface ILessonService : IEntityService<Lesson>
    {
        Task<IEnumerable<Lesson>> GetLessonsByGroupId(int id);
        void SaveMaterial(int id, byte[] content);
        void DeleteMaterial(int id);
    }
}
