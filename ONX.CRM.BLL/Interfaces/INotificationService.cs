using System.Threading.Tasks;

namespace ONX.CRM.BLL.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string userEmail, string userFirstName, 
            string userLastName, string userPass, string subject);
    }
}
