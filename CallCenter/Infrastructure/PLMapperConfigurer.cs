using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CallCenterBLL.DTO;
using CallCenter.Models;
using CallCenter.Models.Filter;
using CallCenterDAL;
using CallCenterDAL.Entities;
using HolodDAL.Filtering;

namespace CallCenter.Infrastructure
{
    public class PLMapperConfigurer
    {
        private static MapperConfiguration _mapConfig;

        public static IMapper Mapper { get; private set; }

        public static string TimeFormat { get;set; }

        public static string DateAndTimeFormat { get; set; }

        static private string UserInPhoneStatusToString(UserInPhoneStatus status)
        {
            switch (status)
            {
                case UserInPhoneStatus.Called:
                    return "C";
                case UserInPhoneStatus.Accepted:
                    return "A";
                case UserInPhoneStatus.Missed:
                    return "M";
                case UserInPhoneStatus.Dropped:
                    return "D";
                case UserInPhoneStatus.Error:
                    return "E";
                default:
                    throw new ArgumentException("Unknown status");
            }
        }

        static PLMapperConfigurer()
        {
            TimeFormat = @"hh\:mm\:ss";
            DateAndTimeFormat = "dd.MM.yyyy HH:mm:ss";

            _mapConfig = new MapperConfiguration(
                cfg =>
                    {
                        cfg.CreateMap<PhoneCallDTO, PhoneCallPL>()
                            .ForMember(destinationMember => destinationMember.UserInfo,
                                opt => opt.MapFrom(x =>
                                    String.Join(
                                        ", ",
                                        x.UserInfoList.Select(y => String.Format(
                                            "Id({0}) Ph({1}) S({2})",
                                            y.Id,
                                            y.Phone,
                                            UserInPhoneStatusToString(y.Status)))
                                            .ToList()
                                    )))
                            .ForMember(destinationMember => destinationMember.Status,
                                opt => opt.MapFrom(x => x.Status.GetDescription()))
                            .ForMember(destinationMember => destinationMember.Duration,
                            opt => opt.MapFrom(x => (x.DurationSeconds == null? String.Empty: new TimeSpan(0, 0, (int)x.DurationSeconds).ToString())))
                            .ForMember(destinationMember => destinationMember.ChildCallIds,
                                opt => opt.MapFrom(x => String.Join(", ", x.ChildCallIds)))
                            .ForMember(destinationMember => destinationMember.StartTime,
                                opt => opt.MapFrom(x => x.StartTime.ToString(DateAndTimeFormat)))
                            .ForMember(destinationMember => destinationMember.TerminationTime,
                                opt => opt.MapFrom(x => x.TerminationTime.ToString(DateAndTimeFormat)))
                            .ForMember(destinationMember => destinationMember.ConnectionTime,
                                opt => opt.MapFrom(x => (x.ConnectionTime == null ? String.Empty : ((DateTime)x.ConnectionTime).ToString(DateAndTimeFormat))));

                        cfg.CreateMap<FilterJqdrid, HolodDAL.Filtering.Filter>()
                            .ForMember(destinationMember => destinationMember.GroupOperator, opt => opt.MapFrom(x => x.groupOp))
                            .ForMember(destinationMember => destinationMember.Rules, opt => opt.MapFrom(x => x.rules));

                        cfg.CreateMap<FilterRuleJqgrid, FilterRule>()
                            .ForMember(destinationMember => destinationMember.Operator, opt => opt.MapFrom(p => p.op))
                            .ForMember(destinationMember => destinationMember.PropertyName, opt => opt.MapFrom(p => p.field))
                            .ForMember(destinationMember => destinationMember.PropertyValue, opt => opt.MapFrom(p => p.data));
                    }
                );

            Mapper = _mapConfig.CreateMapper();
        }
    }
}