using ONX.CRM.ViewModel.PageInfo;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.ViewModel
{
    public class TeacherViewModel : PersonViewModel
    {
        public string WorkExperience { get; set; }
        public string Bio { get; set; }
        public SearchTeacherViewModel Search { get; set; }
        public PageInfoViewModel PageInfo { get; set; }
    }
}
