using System;
using CallCenterDAL.Entities;


namespace CallCenterBLL.DTO
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public UserInPhoneStatus Status { get; set; }
    }
}
