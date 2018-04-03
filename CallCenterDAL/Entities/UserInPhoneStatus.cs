using System;
using System.ComponentModel;

namespace CallCenterDAL.Entities
{
    public enum UserInPhoneStatus
    {
        [Description("Позвонил")]
        Called,

        [Description("Принял")]
        Accepted,

        [Description("Пропустил")]
        Missed,

        [Description("Сбросил")]
        Dropped,

        [Description("Ошибка")]
        Error
    }
}
