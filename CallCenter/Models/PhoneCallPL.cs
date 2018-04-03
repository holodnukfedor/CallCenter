using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenterDAL.Entities;

namespace CallCenter.Models
{
    public class PhoneCallPL
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string StartTime { get; set; }

        public string ConnectionTime { get; set; }

        public string TerminationTime { get; set; }

        public string Duration { get; set; }

        public string UserInfo { get; set; }

        public string ParentCallId { get; set; }

        public string ChildCallIds { get; set; }
    }
}