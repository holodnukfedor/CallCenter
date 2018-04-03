using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace CallCenterDAL.Entities
{
    public enum PhoneCallStatus
    {
        [Description("Брошен")]
        Dropped = 0,

        [Description("Пропущен")]
        Missed = 1,

        [Description("Соединен")]
        Connected = 2,

        [Description("Ошибка")]
        Error = 3,

        [Description("Ошибка после соединения")]
        ErrorAfterConnection = 4
    }
}
