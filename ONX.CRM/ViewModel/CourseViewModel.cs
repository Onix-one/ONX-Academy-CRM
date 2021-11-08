using System.Collections.Generic;

namespace ONX.CRM.ViewModel
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NecessaryPreKnowledge { get; set; }
        public decimal Cost { get; set; }
        public int SpecializationId { get; set; }
        public SpecializationViewModel Specialization { get; set; }
        public List<GroupViewModel> Groups { get; set; }
        public int RequestsCount { get; set; }
    }
}