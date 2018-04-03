using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolodDAL.Repositories;
using CallCenterDAL.Repositories.Interfaces;
using CallCenterDAL.Entities;
using CallCenterDAL.Context;

namespace CallCenterDAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private CallCenterDbContext _context;

        private bool _disposed = false;

        private PhoneCallRepository _phoneCallRepository;

        private UserRepository _userRepository;

        private UserInPhoneCallRepository _userInPhoneCallRepository;

        public IRepository<PhoneCall> PhoneCalls
        {
            get
            {
                return _phoneCallRepository ?? (_phoneCallRepository = new PhoneCallRepository(_context));
            }
        }

        public IRepository<User> Users
        {
            get
            {
                return _userRepository ?? (_userRepository = new UserRepository(_context));
            }
        }

        public IRepository<UserInPhoneCall> UserInPhoneCalls
        {
            get
            {
                return _userInPhoneCallRepository ?? (_userInPhoneCallRepository = new UserInPhoneCallRepository(_context));
            }
        }

        public EFUnitOfWork(string connectionString)
        {
            _context = new CallCenterDbContext(connectionString);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _context.Dispose();

            this._disposed = true;
        }
    }
}
