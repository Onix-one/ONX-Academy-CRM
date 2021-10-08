using ONX.CRM.BLL.Enums;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.ViewModel
{
    public class StudentViewModel : PersonViewModel
    {
        public int? GroupId { get; set; }
        public GroupViewModel Group { get; set; }
        public StudentType? Type { get; set; }
        public SearchStudentViewModel Search { get; set; }

    }
}
