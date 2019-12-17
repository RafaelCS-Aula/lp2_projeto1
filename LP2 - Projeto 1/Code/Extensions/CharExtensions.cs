using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for Character Extensions
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Returns String with repeated characters
        /// </summary>
        /// <param name="c">character</param>
        /// <param name="count">size of string</param>
        /// <returns>String</returns>
        public static string Repeat(this char c, int count) => 
            new string(c, count);
        
        /// <summary>
        /// Converts Character Enumerable to String
        /// </summary>
        /// <param name="c">character enumerable</param>
        /// <returns>String</returns>
        public static string ConvertToString(this IEnumerable<char> c) => 
            new string(c.ToArray());

        /// <summary>
        /// Returns String Array from Enumerable of char Enumerable 
        /// </summary>
        /// <param name="enumerable">Enumerable of char Enumerable</param>
        /// <returns>String Array</returns>
        public static string[] ToStringArray(
            this IEnumerable<IEnumerable<char>> enumerable)
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
