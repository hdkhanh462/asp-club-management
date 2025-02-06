using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Shared.Utils
{
    public class OrderHelper
    {
        public static IQueryable<T> OrderByMultiple<T>(
            IQueryable<T> query,
            List<string> order,
            List<string> avalibleProperties)
        {
            var isFirstSort = true;
            foreach (var item in order)
            {
                if (!string.IsNullOrEmpty(item) && avalibleProperties.Contains(item))
                {
                    if (item.StartsWith("-"))
                    {
                        if (isFirstSort)
                        {
                            query = query.OrderByDescending(u => EF.Property<object>(u, item.Substring(1)));
                            isFirstSort = false;
                        }
                        else query = ((IOrderedQueryable<T>)query).ThenByDescending(u => EF.Property<object>(u, item.Substring(1)));

                    }
                    else
                    {
                        if (isFirstSort)
                        {
                            query = query.OrderBy(u => EF.Property<object>(u, item));
                            isFirstSort = false;
                        }
                        else query = ((IOrderedQueryable<T>)query).ThenBy(u => EF.Property<object>(u, item));
                    }
                }
            }

            return query;
        }
    }
}
