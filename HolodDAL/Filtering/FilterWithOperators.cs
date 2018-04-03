using System;

namespace HolodDAL.Filtering
{
    public class FilterWithOperators
    {
        public Filter Filter { get; private set; }

        public IFilterOperators FilterOperators { get; private set; }

        public FilterWithOperators(Filter filter, IFilterOperators filterOperators)
	    {
            Filter = filter;
            FilterOperators = filterOperators;
	    }
    }
}
