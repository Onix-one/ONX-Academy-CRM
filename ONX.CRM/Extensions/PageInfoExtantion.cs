using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            if (pageSize != 0)
            {
                if (pageNumber != 0)
                {
                    return pageInfo.SetPageSizePageNumberAndTotalItems(pageSize, pageNumber, totalItems);
                }
                return pageInfo.SetPageSizePageNumberAndTotalItems(pageSize, startPageNumber, totalItems);
            }
            return pageInfo.SetPageSizePageNumberAndTotalItems(startPageSize, startPageNumber, totalItems);
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
