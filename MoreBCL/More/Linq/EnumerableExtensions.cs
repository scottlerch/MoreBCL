namespace More.Linq
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// Partitions a long list into a list of smaller lists.
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> PartitionBy<T>(this IEnumerable<T> self, long size)
        {
            var list = new List<T>();
            long count = 0;

            if (size <= 0)
            {
                yield break;
            }

            foreach (var item in self)
            {
                if (count >= size)
                {
                    yield return list;
                    list = new List<T>();
                    count = 0;
                }

                list.Add(item);
                count++;
            }

            if (list.Count > 0)
            {
                yield return list;
            }
        }
    }
}