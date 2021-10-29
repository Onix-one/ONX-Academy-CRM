using System.Collections.Generic;

namespace ONX.CRM.DAL.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PNGName { get; set; }
        public string Description { get; set; }
        public List<Course> Courses { get; set; }

    }
}
