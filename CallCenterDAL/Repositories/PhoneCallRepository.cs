using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallCenterDAL.Repositories.Interfaces;
using HolodDAL.Sorting;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using HolodDAL.Filtering;
using CallCenterDAL.Context;
using CallCenterDAL.Entities;

namespace CallCenterDAL.Repositories
{
    public class PhoneCallRepository : AbstractEFRepository<PhoneCall>
    {
        private CallCenterDbContext _dbContext;

        protected override string DefaultOrderProperty { get { return "StartTime"; } }

        protected override DbSet<PhoneCall> DbSet { get { return _dbContext.PhoneCalls; } }

        protected override IQueryable<PhoneCall> Queryable
        {
            get
            {
                return _dbContext.PhoneCalls
                    .Include("UserInPhoneCall")
                    .Include("UserInPhoneCall.User")
                    .Include("ChildPhoneCalls");
            }
        }

        protected override DbContext DbContext { get { return _dbContext; } }

        public PhoneCallRepository(CallCenterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(PhoneCall item)
        {
            throw new NotImplementedException("It is nessasary create validation rules");
        }

        public void Update(PhoneCall item)
        {
            throw new NotImplementedException("It is nessasary create validation rules");
        }

        public override IEnumerable<PhoneCall> FindAmount(FilterWithOperators filter, int fromRow, int amount, String orderPropertyName, SortOrder sortOrder = SortOrder.Asc)
        {
            return base.FindAmount(filter, fromRow, amount, orderPropertyName, sortOrder);
        }
    }
}
