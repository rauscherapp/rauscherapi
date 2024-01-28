using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace Domain.QueryParameters
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByString = string.Empty;

            foreach (var orderByClause in (string[])orderBy.Split(','))
            {
                var trimmedOrderByClause = orderByClause.Trim();
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                orderByString = orderByString +
                    (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                    + propertyName
                    + (orderDescending ? " descending" : " ascending");
            }

            return source.OrderBy(orderByString);
        }
    }
}
