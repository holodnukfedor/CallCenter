using System;
using System.Collections.Generic;
using System.Linq;
using HolodDAL.Repositories;
using CallCenterDAL.Entities;

namespace CallCenterDAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<PhoneCall> PhoneCalls { get; }
        IRepository<User> Users { get; }
        IRepository<UserInPhoneCall> UserInPhoneCalls { get; }
        void Save();
    }
}
