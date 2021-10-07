using System.Collections.Generic;

namespace ONX.CRM.ViewModel
{
    public class RequestsListViewModel
    {
        public IList<StudentRequestViewModel> RequestsList { get; set; }
        public int GroupId { get; set; }
    }
}
