
using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Common.Extentions
{

    public static class IQueryableExtensions
    {
        public async static Task<PagedResult<T>> GetPagedResultAsync<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            var rowCount = await query.CountAsync();
            var results = await query.Skip(skip).Take(take).ToListAsync();

            var pagedResult = new PagedResult<T>
            {
                CurrentPage = currentPage,
                PageCount = (int)Math.Ceiling(decimal.Divide(rowCount, pageSize)),
                PageSize = pageSize,
                RowCount = rowCount,
                Results = results,
            };

            return pagedResult;
        }


        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string Field, string Direction)
        {
            if (Field != null)
            {



                var prop = typeof(T).GetProperties().FirstOrDefault(a => a.Name.ToLower() == Field.ToLower());
                if (prop != null)
                {
                    ParameterExpression parameter = Expression.Parameter(typeof(T), "s");
                    MemberExpression property = Expression.Property(parameter, prop);
                    LambdaExpression sort = Expression.Lambda(property, parameter);

                    MethodCallExpression call = Expression.Call(
                                                             typeof(Queryable),
                                                             Direction == "asc" ? "OrderBy" : "OrderByDescending",
                                                             new[] { typeof(T), property.Type },
                                                             query.Expression,
                                                             Expression.Quote(sort));


                    query = (IOrderedQueryable<T>)query.Provider.CreateQuery<T>(call);
                }


            }
            return query;
        }
    }
}
