using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> NullSafeWhere<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate)
        {
            return predicate is null ? source : source.Where(predicate);
        }
    }
}
