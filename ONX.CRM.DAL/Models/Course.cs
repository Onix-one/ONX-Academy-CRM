using System.Collections.Generic;

namespace ONX.CRM.DAL.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NecessaryPreKnowledge { get; set; }
        public decimal Cost { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public List<Group> Groups { get; set; }
    }
}
