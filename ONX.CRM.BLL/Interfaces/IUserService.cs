using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User user, string role);
        Task Delete(string id);
        Task Update(string id, string email, string firstName, string lastName);
    }
}
