using System;

namespace HolodDAL.Sorting
{
    public class SortOrderConverter
    {
        public static SortOrder GetSortOrderFromString(String sortOrder)
        {
            if (sortOrder.ToLower() == "asc") return SortOrder.Asc;
            return SortOrder.Desc;
        }
    }
}
