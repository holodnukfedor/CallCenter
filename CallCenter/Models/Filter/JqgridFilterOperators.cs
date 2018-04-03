using System;
using HolodDAL.Filtering;

namespace CallCenter.Models.Filter
{
    public class JqgridFilterOperators : IFilterOperators
    {
        public string AndOperator { get { return "AND"; } }
        public string OrOperator { get { return "OR"; } }
        public string EqualOperator { get { return "eq"; } }
        public string NotEqualOperator { get { return "ne"; } }
        public string Lesser { get { return "lt"; } }
        public string Greater { get { return "gt"; } }
        public string GreaterOrEqualOperator { get { return "ge"; } }
        public string LessOrEqualOperator { get { return "le"; } }
        public string ContainsOperator { get { return "cn"; } }
        public string UserDefinedOperator { get { return "custom"; } }
    }
}