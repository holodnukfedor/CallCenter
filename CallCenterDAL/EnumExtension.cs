using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CallCenterDAL
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();

            if(!type.IsEnum)
            {
                throw new ArgumentException("enumerationValue must be of Enum type");
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if(memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if(attrs.Length > 0)
                {
                return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
    }
}
