namespace Api.Extensions
{
    public static class PaginationExtensions
    {

        /// <summary>
        /// Returns the page in the pagination.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">the query</param>
        /// <param name="page">Page that is requested</param>
        /// <param name="ItemCount">Items per page</param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query,int page,int ItemCount)
        {
            return query.Skip(ItemCount * page).Take(ItemCount);
        }

        /// <summary>
        /// Returns pages in pagination
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">the query</param>
        /// <param name="ItemCount">Items per page</param>
        /// <returns>count of pages</returns>
        public static int PageCount<T>(this IQueryable<T> query,int ItemCount)
        {
            int count = 0;
            for (int i = query.Count(); i > 0;  i -= ItemCount)
            {
                count++;
            }

            return count;
        }
    }
}
