using System;
using System.Collections.Generic;
using System.Linq;
using CallCenterBLL.DTO;
using HolodDAL.Filtering;
using HolodDAL.Sorting;

namespace CallCenterBLL.Services.Interfaces
{
    public interface IPhoneCallService : IDisposable
    {
        IList<PhoneCallDTO> GetPhoneCalls(FilterWithOperators filter, int page, int amount, string sortPropertyName, SortOrder sortOrder, out PaginationInfo paginationInfo);
    }
}
