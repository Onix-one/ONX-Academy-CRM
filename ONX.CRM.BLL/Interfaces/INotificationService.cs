using System.Collections.Generic;
using System.Threading.Tasks;
using ONX.CRM.DAL.Models;

namespace ONX.CRM.BLL.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string userEmail, string userFirstName, 
            string userLastName, string userPass, string subject);
    }
}
