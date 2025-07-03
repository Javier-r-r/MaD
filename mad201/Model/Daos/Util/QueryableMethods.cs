using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Model.Daos.Util
{
    public static class QueryableMethods
    {
        public static PagedResult<T> ToPagedList<T>(this IQueryable<T> query, int pageNumber = 1, int pageSize = 10)
        {
            // Usamos Reflection para crear dinámicamente la expresión de ordenación
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, "Id");  // Aquí 'Id' es el nombre de la propiedad

            // Especificamos explícitamente el tipo 'long' si 'Id' es de tipo long
            var lambda = Expression.Lambda<Func<T, long>>(property, parameter);

            // Ordenamos la consulta usando la expresión generada
            query = query.OrderBy(lambda);

            int totalItems = query.Count();

            List<T> items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<T>(items, totalItems, pageNumber, pageSize);
        }
    }
}
