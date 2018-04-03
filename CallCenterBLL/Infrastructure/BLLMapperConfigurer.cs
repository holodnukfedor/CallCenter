using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CallCenterDAL.Entities;
using CallCenterBLL.DTO;

namespace CallCenterBLL.Infrastructure
{

    public class BLLMapperConfigurer
    {
        private static MapperConfiguration _mapConfig;

        public static IMapper Mapper { get; private set; }

        static BLLMapperConfigurer()
        {
            _mapConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PhoneCall, PhoneCallDTO>()
                        .ForMember(destinationMember => destinationMember.UserInfoList,
                            opt => opt.MapFrom(x => x.UserInPhoneCall.Select(u => new UserInfo { Id = u.User.Id, Phone = u.User.Phone, Status = u.Status })))
                        .ForMember(destinationMember => destinationMember.ChildCallIds,
                            opt => opt.MapFrom(x => x.ChildPhoneCalls.Select(p => p.Id)));
                }
            );

            Mapper = _mapConfig.CreateMapper();
        }
    }
}
