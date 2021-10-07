using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONX.CRM.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
