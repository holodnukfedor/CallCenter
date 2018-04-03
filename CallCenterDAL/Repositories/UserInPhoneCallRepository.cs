using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenterDAL.Repositories.Interfaces;
using HolodDAL.Sorting;
using System.Data.Entity;
using CallCenterDAL.Context;
using CallCenterDAL.Entities;

namespace CallCenterDAL.Repositories
{
    public class UserInPhoneCallRepository : AbstractEFRepository<UserInPhoneCall>
    {
        private CallCenterDbContext _dbContext;

        protected override string DefaultOrderProperty { get { return "UserId"; } }

        protected override DbSet<UserInPhoneCall> DbSet { get { return _dbContext.UserInPhoneCalls; } }

        protected override IQueryable<UserInPhoneCall> Queryable
        {
            get
            {
                return DbSet;
            }
        }

        protected override DbContext DbContext { get { return _dbContext; } }

        public UserInPhoneCallRepository(CallCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
