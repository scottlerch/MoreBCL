namespace More.Collections.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Extension methods for LinkedList{T}.
    /// </summary>
    public static class LinkedListExtensions
    {
        /// <summary>
        /// Enumerate nodes in a linked list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<LinkedListNode<T>> EnumerateNodes<T>(this LinkedList<T> list)
        {
            var node = list.First;

            while (node != null)
            {
                yield return node;
                node = node.Next;
            }
        }
    }
}
