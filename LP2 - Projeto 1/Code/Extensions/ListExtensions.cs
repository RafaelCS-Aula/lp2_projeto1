using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    public static class ListExtensions
    {
        public static List<KeyValuePair<Title, Rating>> Sort<TKey>(
            this IList<KeyValuePair<Title, Rating>> enumerable,
            Func<KeyValuePair<Title, Rating>, TKey> keySelector)
        {
            return enumerable.OrderBy(keySelector).ToList();
        }
    }
}
