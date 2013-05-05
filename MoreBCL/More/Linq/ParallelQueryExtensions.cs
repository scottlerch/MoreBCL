namespace More.Linq
{
    using System.Linq;

    /// <summary>
    /// Extension methods for ParallelQuery.
    /// </summary>
    public static class ParallelQueryExtensions
    {
        /// <summary>
        /// Parallelize query as ordered or not.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="ordered">if set to <c>true</c> ordered.</param>
        /// <returns></returns>
        public static ParallelQuery<TSource> AsOrdered<TSource>(
            this ParallelQuery<TSource> source,
            bool ordered)
        {
            return ordered ? source.AsOrdered() : source.AsUnordered();
        }
    }
}
