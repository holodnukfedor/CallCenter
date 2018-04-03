using System;

namespace HolodDAL.Filtering
{
    public class FilterRule
    {
        public String PropertyName { get; set; }

        public String Operator { get; set; }

        public String PropertyValue { get; set; }
    }
}
