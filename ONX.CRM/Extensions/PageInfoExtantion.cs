using ONX.CRM.ViewModel.PageInfo;

namespace ONX.CRM.Extensions
{
    public static class PageInfoExtension
    {
        public static PageInfoViewModel CheckingPageInfo(this PageInfoViewModel pageInfo,
            int pageSize, int pageNumber, int totalItems)
        {
            const int startPageSize = 10;
            const int startPageNumber = 1;

            return pageInfo.SetPageSizePageNumberAndTotalItems(
                pageSize != 0 ? pageSize : startPageSize, 
                pageNumber != 0 ? pageNumber : startPageNumber, totalItems);
        }
        private static PageInfoViewModel SetPageSizePageNumberAndTotalItems(this PageInfoViewModel pageInfo,
            int pageSize, int pageNumber, int totalItems)
        {
            pageInfo.PageSize = pageSize;
            pageInfo.PageNumber = pageNumber;
            pageInfo.TotalItems = totalItems;
            return pageInfo;
        }
    }
}
