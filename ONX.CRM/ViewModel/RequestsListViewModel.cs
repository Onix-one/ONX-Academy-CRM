using System.Collections.Generic;
using ONX.CRM.ViewModel.Search;

namespace ONX.CRM.ViewModel
{
    public class RequestsListViewModel
    {
        public IList<StudentRequestViewModel> RequestsList { get; set; }
        public int GroupId { get; set; }
        public SearchRequestViewModel Search { get; set; }
    }
}
