using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenterDAL.Entities
{
    public class PhoneCall
    {
        public int Id { get; set; }

        public PhoneCallStatus Status { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? ConnectionTime { get; set; }

        public DateTime TerminationTime { get; set; }

        public int? ParentCallId { get; set; }

        public int? DurationSeconds { get; set; }

        public virtual PhoneCall ParentCall { get; set; }

        public virtual ICollection<PhoneCall> ChildPhoneCalls { get; set; }

        public virtual ICollection<UserInPhoneCall> UserInPhoneCall { get; set; }
    }
}
