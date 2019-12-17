using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    public static class CharExtensions
    {
        public static string Repeat(this char c, int count)
        {
            return new string(c, count);
        }

        public static string ConvertToString(this IEnumerable<char> c)
        {
            return new string(c.ToArray());
        }

        public static string[] ToStringArray(this IEnumerable<IEnumerable<char>> enumerable)
        {
            string[] s = new string[enumerable.Count()];
            int i = 0;
            foreach (char[] c in enumerable)
            {
                s[i] = new string(c);
                i++;
            }

            return s;
        }
    }
}
