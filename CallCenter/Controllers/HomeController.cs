using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterBLL.Services.Interfaces;
using CallCenterBLL.DTO;
using CallCenter.Models.Filter;
using HolodDAL.Sorting;
using HolodDAL.Filtering;
using CallCenter.Models;
using CallCenter.Infrastructure;

namespace CallCenter.Controllers
{
    public class HomeController : Controller
    {
        private IPhoneCallService _phoneCallService;

        public HomeController(IPhoneCallService phoneCallService)
        {
            _phoneCallService = phoneCallService;
        }

        protected override void Dispose(bool disposing)
        {
            _phoneCallService.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public JsonResult GetPhoneCallsJsonList(bool _search, int page, int rows, string sidx, string sord, string filters)
        {
            FilterJqdrid jqgridFilter;
            HolodDAL.Filtering.Filter filter = null;

            if (_search)
            {
                jqgridFilter = FilterJqdrid.DeserializeJson(filters);
                filter = PLMapperConfigurer.Mapper.Map<FilterJqdrid, HolodDAL.Filtering.Filter>(jqgridFilter);
            }

            PaginationInfo paginationInfo;
            IList<PhoneCallDTO> phoneCallsDTOList = _phoneCallService.GetPhoneCalls(new FilterWithOperators(filter, new JqgridFilterOperators()), page, rows, sidx, SortOrderConverter.GetSortOrderFromString(sord), out paginationInfo);
            IList<PhoneCallPL> phoneCallsPLList = PLMapperConfigurer.Mapper.Map<IList<PhoneCallDTO>, IList<PhoneCallPL>>(phoneCallsDTOList);
            PhoneCallsListForJqgrid result = new PhoneCallsListForJqgrid(paginationInfo, phoneCallsPLList);

            return this.Json(result, JsonRequestBehavior.AllowGet);  
        }

        public ActionResult GetPhoneStatusesDropdown()
        {
            return PartialView();
        }
    }
}