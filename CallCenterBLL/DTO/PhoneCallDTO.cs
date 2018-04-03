using System;
using System.Collections.Generic;
using CallCenterDAL.Entities;

namespace CallCenterBLL.DTO
{
    public class PhoneCallDTO
    {
        public int Id { get; set; }

        public PhoneCallStatus Status { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? ConnectionTime { get; set; }

        public DateTime TerminationTime { get; set; }

        public List<UserInfo> UserInfoList { get; set; }

        public int? DurationSeconds { get; set; }

        public int? ParentCallId { get; set; }

        public List<int> ChildCallIds { get; set; }
    }
}
