using ONX.CRM.DAL.Enums;

namespace ONX.CRM.DAL.Models
{
    public class Student : Person
    {
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        public StudentType? Type { get; set; }
    }

}
