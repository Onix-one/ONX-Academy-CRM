using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IManagerService : IEntityService<Manager>
    {
        Task<bool> CheckIfManagerExists(string email);
        Task<IEnumerable<Manager>> FindByUserIdAsync(string userId);
        void SaveImage(int id, byte[] content);
        void DeleteImage(int id);
    }
}
