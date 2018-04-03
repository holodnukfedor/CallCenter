using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.ComponentModel;

namespace HolodDAL.Filtering
{
    public static class WhereExtension
    {
        private static string GetConditionString<T>(FilterRule rule, IFilterOperators filterOperators, ref int parameterNumber)
        {
            if (rule.Operator == filterOperators.EqualOperator)
            {
                return String.Format("x.{0} == @{1}", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.NotEqualOperator)
            {
                return String.Format("x.{0} != @{1}", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.Lesser)
            {
                return String.Format("x.{0} < @{1}", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.Greater)
            {
                return String.Format("x.{0} > @{1}", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.LessOrEqualOperator)
            {
                return String.Format("x.{0} <= @{1}", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.GreaterOrEqualOperator)
            {
                return String.Format("x.{0} >= @{1}", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.ContainsOperator)
            {
                return String.Format("x.{0}.Contains(@{1})", rule.PropertyName, parameterNumber++);
            }
            else if (rule.Operator == filterOperators.UserDefinedOperator)
            {
                return String.Format("x.{0}", rule.PropertyName);
            }
            else
            {
                throw new ArgumentException("Unknown filter operator");
            }
        }

        private static Object[] GetParameterArray<T>(FilterWithOperators filter)
        {
            int countOfRulesWithParameter = filter.Filter.Rules.Count(x => x.Operator != filter.FilterOperators.UserDefinedOperator);

            if (countOfRulesWithParameter == 0)
                return null;

            Object[] parametrArr = new Object[countOfRulesWithParameter];

            int index = 0;
            foreach (var rule in filter.Filter.Rules)
            {
                Type entityType = typeof(T);
                PropertyInfo propertyInfo = entityType.GetProperty(rule.PropertyName);
                Type propertyType = propertyInfo.PropertyType;

                Object propertyValueObject = TypeDescriptor.GetConverter(propertyType).ConvertFrom(rule.PropertyValue);

                parametrArr[index++] = propertyValueObject;
            }
            return parametrArr;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> source, FilterWithOperators filter)
        {
            if (filter.Filter == null || filter.Filter.Rules.Count == 0)
                return source;

            String[] conditionArr = new String[filter.Filter.Rules.Count];
            Object[] parametrArr = GetParameterArray<T>(filter);

            int parameterNumber = 0;
            int conditionIndex = 0;
            foreach (var rule in filter.Filter.Rules)
                conditionArr[conditionIndex++] = GetConditionString<T>(rule, filter.FilterOperators, ref parameterNumber);
            
            string fullCondition = String.Join(
                (filter.Filter.GroupOperator == filter.FilterOperators.AndOperator? " and ": " or "),
                conditionArr
            );

            if (parametrArr == null)
                return source.Where(String.Format("x => {0}", fullCondition));

            return source.Where(String.Format("x => {0}", fullCondition), parametrArr);
        }
    }
}
