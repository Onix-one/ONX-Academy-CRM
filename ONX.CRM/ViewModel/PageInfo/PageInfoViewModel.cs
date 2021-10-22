using System;

namespace ONX.CRM.ViewModel.PageInfo
{
    public class PageInfoViewModel
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize); // всего страниц
    }
}
