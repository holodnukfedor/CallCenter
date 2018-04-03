using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CallCenterDAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CallCenterDAL.Context
{
    public class CallCenterDbContext : DbContext
    {
        public DbSet<PhoneCall> PhoneCalls { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInPhoneCall> UserInPhoneCalls { get; set; }

        static CallCenterDbContext()
        {
            Database.SetInitializer<CallCenterDbContext>(new CallCenterDbInitializer());
        }

        public CallCenterDbContext(string connectionString)
            : base(connectionString)
        {

        }
    }
}
