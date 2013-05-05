namespace More.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// This class represents an unordered bag of items with the
    /// the capability to get a random item.  All operations are O(1).
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public class Bag<T> : ICollection<T>, ICollection
    {
        private readonly List<T> items;
        private readonly Dictionary<T, int> itemsIndex;

        private Random rand;
        private object syncRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bag{T}"/> class.
        /// </summary>
        public Bag()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bag{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public Bag(int capacity)
        {
            this.itemsIndex = new Dictionary<T, int>(capacity);
            this.items = new List<T>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public Bag(IEnumerable<T> collection)
        {
            this.items = new List<T>(collection);
            this.itemsIndex = this.items
                .Select((value, index) => new { value, index })
                .ToDictionary(pair => pair.value, pair => pair.index);
        }

        /// <summary>
        /// Get random item from bag.
        /// </summary>
        /// <returns>Random item from bag.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// The bag is empty.
        /// </exception>
        public T Random()
        {
            if (this.items.Count == 0)
            {
                throw new InvalidOperationException();
            }

            if (this.rand == null)
            {
                this.rand = new Random();
            }

            var randomIndex = this.rand.Next(0, this.items.Count);
            return this.items[randomIndex];
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            this.itemsIndex.Add(item, this.items.Count);
            this.items.Add(item);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            // Replace index of value to remove with last item in values list
            var keyIndex = this.itemsIndex[item];
            var lastItem = this.items[this.items.Count - 1];
            this.items[keyIndex] = lastItem;

            // Update index in dictionary for last item that was just moved
            this.itemsIndex[lastItem] = keyIndex;

            // Remove old value
            this.itemsIndex.Remove(item);
            this.items.RemoveAt(this.items.Count - 1);

            return true;
        }

        /// <inheritdoc />
        public bool Contains(T item)
        {
            return this.itemsIndex.ContainsKey(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            this.itemsIndex.Clear();
            this.items.Clear();
        }

        /// <inheritdoc />
        public int Count
        {
            get { return this.items.Count; }
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.items).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc />
        public void CopyTo(Array array, int index)
        {
            this.CopyTo(array as T[], index);
        }

        /// <inheritdoc />
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <inheritdoc />
        public object SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(
                        ref this.syncRoot,
                        new object(),
                        null);
                }

                return this.syncRoot;

            }
        }
    }
}