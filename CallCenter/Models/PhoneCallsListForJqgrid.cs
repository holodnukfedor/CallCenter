using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenterBLL.DTO;

namespace CallCenter.Models
{
    public class PhoneCallsListForJqgrid : PaginationInfo
    {
        public IList<PhoneCallPL> PhoneCallList { set; get; }

        public PhoneCallsListForJqgrid(PaginationInfo paginationInfo, IList<PhoneCallPL> phoneCallList)
        {
            PageNumber = paginationInfo.PageNumber;
            PagesCount = paginationInfo.PagesCount;
            RowsCount = paginationInfo.RowsCount;
            PhoneCallList = phoneCallList;
        }
    }
}