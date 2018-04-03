using System;
using System.Collections.Generic;
using System.Linq;
using CallCenterBLL.Services.Interfaces;
using CallCenterBLL.DTO;
using HolodDAL.Filtering;
using HolodDAL.Sorting;
using CallCenterDAL.Repositories.Interfaces;
using CallCenterBLL.Infrastructure;
using CallCenterDAL.Entities;

namespace CallCenterBLL.Services
{
    public class PhoneCallService : IPhoneCallService
    {
        private IUnitOfWork _database { get; set; }

        private void ReplaceWithUserDefinedRules(FilterWithOperators filter)
        {
            if (filter.Filter != null && filter.Filter.Rules != null)
            {
                int paramIndex = 0;
                foreach (var rule in filter.Filter.Rules)
                {
                    if (rule.PropertyName == "ChildCallIds")
                    {
                        rule.Operator = filter.FilterOperators.UserDefinedOperator;
                        rule.PropertyName = String.Format("ChildPhoneCalls.Count(p{0} => p{0}.Id == {1}) > 0", paramIndex++, rule.PropertyValue);
                        continue;
                    }
                    if (rule.PropertyName == "UserInfo")
                    {
                        rule.Operator = filter.FilterOperators.UserDefinedOperator;
                        rule.PropertyName = String.Format("UserInPhoneCall.Count(u{0} => u{0}.User.Phone.Contains(\"{1}\")) > 0", paramIndex++, rule.PropertyValue);
                        continue;
                    }
                    if (rule.PropertyName == "Status" && String.IsNullOrWhiteSpace(rule.PropertyValue))
                    {
                        rule.PropertyValue = "0";
                        continue;
                    }
                    if (rule.PropertyName == "Duration")
                    {
                        rule.PropertyName = "DurationSeconds";
                    }
                }
            }
        }

        public PhoneCallService(IUnitOfWork unitOfWork)
        {
            _database = unitOfWork;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public IList<PhoneCallDTO> GetPhoneCalls(FilterWithOperators filter, int page, int amount, string sortPropertyName, SortOrder sortOrder, out PaginationInfo paginationInfo)
        {
            if (sortPropertyName == "Duration")
                sortPropertyName = "DurationSeconds";

            paginationInfo = new PaginationInfo();
            List<PhoneCall> phoneCallList = null;
            List<PhoneCallDTO> phoneCallDTOList = new List<PhoneCallDTO>();

            ReplaceWithUserDefinedRules(filter);

            paginationInfo.RowsCount = _database.PhoneCalls.Count(filter);

            if (paginationInfo.RowsCount == 0)
            {
                paginationInfo.PageNumber = 1;
                paginationInfo.PagesCount = 1;
                return phoneCallDTOList;
            }

            if (amount <= 0)
                amount = 10;

            if (amount > paginationInfo.RowsCount)
                amount = paginationInfo.RowsCount;

            paginationInfo.AmountOnPage = amount;
            paginationInfo.PagesCount = paginationInfo.RowsCount / amount;
            if (paginationInfo.RowsCount % amount > 0)
                ++paginationInfo.PagesCount;

            paginationInfo.PageNumber = page;
            if (paginationInfo.PageNumber > paginationInfo.PagesCount)
                paginationInfo.PageNumber = paginationInfo.PagesCount;

            if (paginationInfo.PageNumber <= 0)
                paginationInfo.PageNumber = 1;

            phoneCallList = _database.PhoneCalls.FindAmount(filter, (paginationInfo.PageNumber - 1) * amount, amount, sortPropertyName, sortOrder).ToList();

            phoneCallDTOList = BLLMapperConfigurer.Mapper.Map<List<PhoneCall>, List<PhoneCallDTO>>(phoneCallList);
            return phoneCallDTOList;
        }
    }
}
