using System;
using System.Collections.Generic;
using System.Linq;
using HolodDAL.Filtering;
using HolodDAL.Sorting;

namespace HolodDAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string orderFieldName, SortOrder sortOrder);
        IEnumerable<T> GetAmount(int fromRow, int amount, string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc);
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate, string orderFieldName = null, SortOrder sortOrder = SortOrder.Asc);
        IEnumerable<T> FindAmount(Func<T, Boolean> predicate, int fromRow, int amount, string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc);
        IEnumerable<T> FindAmount(FilterWithOperators filter, int fromRow, int amount, string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc);
        int Count();
        int Count(Func<T, Boolean> predicate);
        int Count(FilterWithOperators filter);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
