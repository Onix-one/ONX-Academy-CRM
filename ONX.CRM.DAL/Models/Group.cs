using System;
using System.Collections.Generic;
using ONX.CRM.DAL.Enums;

namespace ONX.CRM.DAL.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? CourseId { get; set; }
        public Course Course { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime? StartDate { get; set; }
        public GroupStatus? Status { get; set; }
        public List<Student> Students { get; set; }
        
    }
}
