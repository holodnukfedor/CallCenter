using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolodDAL.Repositories;
using HolodDAL.Sorting;
using System.Data.Entity;
using HolodDAL.Filtering;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using CallCenterDAL.Entities;

namespace CallCenterDAL.Repositories
{
    public abstract class AbstractEFRepository<T> : IRepository<T> where T : class
    {
        protected abstract string DefaultOrderProperty { get; }

        protected abstract DbSet<T> DbSet { get; }

        protected abstract IQueryable<T> Queryable { get; }

        protected abstract DbContext DbContext { get; }

        public virtual T Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<T> Find(Func<T, bool> predicate, string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc)
        {
            return Queryable.Where(predicate).AsQueryable()
                .OrderBy(String.Format("{0} {1}", (orderPropertyName == null ? DefaultOrderProperty : orderPropertyName), sortOrder.GetDescription()))
                .ToList();
        }

        public virtual IEnumerable<T> FindAmount(Func<T, Boolean> predicate, int fromRow, int amount, string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc)
        {
            return Queryable.Where(predicate).AsQueryable()
                .OrderBy(String.Format("{0} {1}", (orderPropertyName == null ? DefaultOrderProperty : orderPropertyName), sortOrder.GetDescription()))
                .Skip(fromRow)
                .Take(amount)
                .ToList();
        }

        public virtual IEnumerable<T> FindAmount(FilterWithOperators filter, int fromRow, int amount, String orderPropertyName, SortOrder sortOrder = SortOrder.Asc)
        {
            return Queryable.Where(filter)
                .OrderBy(String.Format("{0} {1}", (orderPropertyName == null ? DefaultOrderProperty : orderPropertyName), sortOrder.GetDescription()))
                .Skip(fromRow)
                .Take(amount)
                .ToList();
        }

        public virtual IEnumerable<T> GetAll(string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc)
        {
            return Queryable
                .OrderBy(String.Format("{0} {1}", (orderPropertyName == null ? DefaultOrderProperty : orderPropertyName), sortOrder.GetDescription()))
                .ToList();
        }

        public virtual IEnumerable<T> GetAmount(int fromRow, int amount, string orderPropertyName = null, SortOrder sortOrder = SortOrder.Asc)
        {
            return Queryable
                .OrderBy(String.Format("{0} {1}", (orderPropertyName == null ? DefaultOrderProperty : orderPropertyName), sortOrder.GetDescription()))
                .Skip(fromRow)
                .Take(amount)
                .ToList(); 
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public virtual int Count(Func<T, bool> predicate)
        {
            return DbSet.Count(predicate);
        }

        public virtual int Count(FilterWithOperators filter)
        {
            return DbSet.Where(filter).Count();
        }

        public virtual void Create(T item)
        {
            DbSet.Add(item);
        }

        public virtual void Update(T item)
        {
            DbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            T item = Get(id);

            if (item != null)
                DbSet.Remove(item);
        }
    }
}
