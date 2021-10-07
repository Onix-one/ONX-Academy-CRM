using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.ViewModel
{
    public class TeacherViewModel : PersonViewModel
    {
        public string PNGName { get; set; }
        public string WorkExperience { get; set; }
        public string Bio { get; set; }
        public SearchTeacherViewModel Search { get; set; }
    }
}
