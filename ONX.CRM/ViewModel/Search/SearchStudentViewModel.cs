using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ONX.CRM.BLL.Enums;

namespace ONX.CRM.ViewModel.Search
{
    public class SearchStudentViewModel : SearchViewModel
    {
        public int CourseId { get; set; }
        public string Type { get; set; }
    }
}
