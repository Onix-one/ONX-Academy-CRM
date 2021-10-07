using ONX.CRM.BLL.Enums;

namespace ONX.CRM.ViewModel
{
    public class StudentViewModel : PersonViewModel
    {
        public int? GroupId { get; set; }
        public GroupViewModel Group { get; set; }
        public StudentType? Type { get; set; }

    }
}
