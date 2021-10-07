using System.Collections.Generic;

namespace ONX.CRM.ViewModel
{
    public class SpecializationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PNGName { get; set; }
        public string Description { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
