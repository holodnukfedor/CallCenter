using System;
using System.Collections.Generic;
using System.Linq;

namespace CallCenterDAL.Entities
{
    public class UserInPhoneCall
    {
        public int Id { get; set; }

        public int PhoneCallId { get; set; }

        public int UserId { get; set; }

        public UserInPhoneStatus Status { get; set; }

        public virtual PhoneCall PhoneCall { get; set; }

        public virtual User User { get; set; }
    }
}
