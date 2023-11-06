using NinjaNye.SearchExtensions;
using System.Linq.Expressions;

namespace Constack.Widemapp.Common.Extensions
{
    public static class FilterDataExtension
    {
        public static IQueryable<T> SearchFromInput<T>(this IQueryable<T> query, string value, params Expression<Func<T, string>>[] stringProperties)
        {
            if (string.IsNullOrEmpty(value)) return query;

            var newQuery = query.Search(stringProperties);

            string[] searchParams = value.Trim().Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => x.ToLower()).ToArray();

            return newQuery.StartsWith(searchParams);
        }

        public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> query, int page, int pageSize, out int totalPages)
        {
            if (page <= 0 || pageSize <= 0)
            {
                totalPages = 1;
                return query.Skip(0);
            }

            else totalPages = (int)Math.Ceiling(((double)query.Count() / (double)pageSize));

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
