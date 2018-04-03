using System;
using Ninject.Modules;
using CallCenterBLL.Services;
using CallCenterBLL.Services.Interfaces;

namespace CallCenter.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IPhoneCallService>().To<PhoneCallService>();
        }
    }
}