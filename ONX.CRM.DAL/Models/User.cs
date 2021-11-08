using Microsoft.AspNetCore.Identity;

namespace ONX.CRM.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
