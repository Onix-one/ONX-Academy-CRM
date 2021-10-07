
using Microsoft.AspNetCore.Identity;

namespace ONX.CRM.BLL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
