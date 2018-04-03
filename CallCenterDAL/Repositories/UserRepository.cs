using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CallCenterDAL.Repositories.Interfaces;
using HolodDAL.Sorting;
using System.Data.Entity;
using CallCenterDAL.Entities;
using CallCenterDAL.Context;

namespace CallCenterDAL.Repositories
{
    public class UserRepository : AbstractEFRepository<User>
    {
        private CallCenterDbContext _dbContext;

        protected override string DefaultOrderProperty { get { return "LastName"; } }

        protected override DbSet<User> DbSet { get { return _dbContext.Users; } }

        protected override IQueryable<User> Queryable
        {
            get
            {
                return DbSet;
            }
        }

        protected override DbContext DbContext { get { return _dbContext; } }

        public UserRepository(CallCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
