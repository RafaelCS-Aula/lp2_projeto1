using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for List Extension
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Sorts a List of Title and Ratings 
        /// </summary>
        /// <typeparam name="TKey">Type to be Sorted</typeparam>
        /// <param name="enumerable">List of Titles and Ratings</param>
        /// <param name="keySelector">The key to be Sorted by</param>
        /// <returns>Sorted List of Titles and Ratings</returns>
        public static List<KeyValuePair<Title, Rating>> Sort<TKey>(
            this IList<KeyValuePair<Title, Rating>> enumerable,
            Func<KeyValuePair<Title, Rating>, TKey> keySelector) => 
            enumerable.OrderBy(keySelector).ToList();
    }
}
