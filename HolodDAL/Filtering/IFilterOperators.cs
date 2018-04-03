using System;

namespace HolodDAL.Filtering
{
    public interface IFilterOperators
    {
        string AndOperator { get; }
        string OrOperator { get; }
        string EqualOperator { get; }
        string NotEqualOperator { get; }
        string Lesser { get; }
        string Greater { get; }
        string GreaterOrEqualOperator { get; }
        string LessOrEqualOperator { get; }
        string ContainsOperator { get; }
        string UserDefinedOperator { get; }
    }
}
