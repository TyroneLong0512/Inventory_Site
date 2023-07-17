using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Extensions
{
    /// <summary>
    /// Extensions for the IEnumerable Interface
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Adds an item to the IEnumerable
        /// </summary>
        /// <typeparam name="TItem">The type of the IEnumerable and item</typeparam>
        /// <param name="input">The IEnumerable to add the item to</param>
        /// <param name="item">The item to add to the IEnumerable</param>
        public static void Add<TItem>(this IEnumerable<TItem> input, TItem item) =>
            ((List<TItem>)input).Add(item);

        /// <summary>
        /// Adds a collection of items to the IEnumerable
        /// </summary>
        /// <typeparam name="TItem">The type of the IEnumerable and item</typeparam>
        /// <param name="input">The IEnumerable to add the item to</param>
        /// <param name="items">The collection of items to add to the IEnumerable</param>
        public static void Add<TItem>(this IEnumerable<TItem> input, IEnumerable<TItem> items) =>
            ((List<TItem>)input).Add(items);

        /// <summary>
        /// Adds an item to an IEnumerable at a specified index
        /// </summary>
        /// <typeparam name="TItem">The type of the IEnumerable</typeparam>
        /// <param name="input">The IEnumerable to operate on</param>
        /// <param name="item">The item to add</param>
        /// <param name="index">The index to add the item at</param>
        public static void AddAt<TItem>(this IEnumerable<TItem> input, TItem item, int index)
        {
            IEnumerable<TItem> items = new List<TItem>();
            items.Add(input.Take(index));
            items.Add(item);
            items.Add(input.TakeLast(input.Count() - index + 1));
            input = items;
        }

        /// <summary>
        /// Removes an item for an IEnumerable at a specified index
        /// </summary>
        /// <typeparam name="TItem">The type of the IEnumerable</typeparam>
        /// <param name="input">The IEnumerable to operate on</param>
        /// <param name="index">The index of the item to remove</param>
        public static void RemoveAt<TItem>(this IEnumerable<TItem> input, int index) =>
            ((List<TItem>)input).RemoveAt(index);

        /// <summary>
        /// Removes all elements after a specified element
        /// </summary>
        /// <typeparam name="TItem">The type of the IEnumerable</typeparam>
        /// <param name="input">The IEnumerable to operate on</param>
        /// <param name="item">The item to remove from</param>
        public static void RemoveAfter<TItem>(this IEnumerable<TItem> input, TItem item)
        {
            input = input.Take(((List<TItem>)input).FindIndex(itm => itm.Equals(item)));
        }
    }
}
