using ONX.CRM.DAL.Enums;
using ONX.CRM.ViewModel.PageInfo;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.ViewModel
{
    public class StudentViewModel : PersonViewModel
    {
        public int? GroupId { get; set; }
        public GroupViewModel Group { get; set; }
        public StudentType? Type { get; set; }
        public SearchStudentViewModel Search { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
    }
}
