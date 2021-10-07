using System;
using ONX.CRM.BLL.Enums;

namespace ONX.CRM.BLL.Models
{
    public class StudentRequest : Person
    {
        public DateTime Created { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public StudentType? Type { get; set; }
        public string Comments { get; set; }
    }
}
