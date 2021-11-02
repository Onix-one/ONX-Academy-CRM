using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IManagerService : IEntityService<Manager>
    {
        Task<bool> CheckIfManagerExists(string email);
    }
}
