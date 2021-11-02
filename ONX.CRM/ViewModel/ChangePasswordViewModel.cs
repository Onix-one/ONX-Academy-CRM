
using System.ComponentModel.DataAnnotations;

namespace ONX.CRM.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        public string OldPassword { get; set; }
    }
}
