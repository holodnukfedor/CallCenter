using System;
using System.Collections.Generic;

namespace HolodDAL.Filtering
{
    public class Filter
    {
        public String GroupOperator { get; set; }

        public List<FilterRule> Rules { get; set; }
    }
}
