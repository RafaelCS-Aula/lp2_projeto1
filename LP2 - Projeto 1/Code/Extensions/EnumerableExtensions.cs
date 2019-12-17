using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for Enumerable Extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns String from String Enumerable
        /// </summary>
        /// <param name="enumerable">String Enumerable</param>
        /// <param name="size">Max Number of String Items</param>
        /// <returns>String from String Enumerable</returns>
        public static string ToStringArray(
            this IEnumerable<string> enumerable, 
            int size = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte i = 0;
            int writersCount = enumerable.Count();
            if (size > 0)
                writersCount = Math.Min(size, writersCount);
            foreach (string s in enumerable)
            {
                stringBuilder.Append(s);

                if (++i < writersCount)
                    stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns String from Char Enumerable
        /// </summary>
        /// <param name="enumerable">Char Enumerable</param>
        /// <param name="size">Max Number of String Items</param>
        /// <returns>String from Char Enumerable</returns>
        public static string ToStringArray(
            this IEnumerable<IEnumerable<char>> enumerable, 
            int size = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte i = 0;
            int writersCount = enumerable.Count();
            if (size > 0)
                writersCount = Math.Min(size, writersCount);
            foreach (IEnumerable<char> s in enumerable)
            {
                stringBuilder.Append(s.ConvertToString());

                if (++i < writersCount)
                    stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Checks if String Enumerable Contains String
        /// </summary>
        /// <param name="enumerable">String Enumerable</param>
        /// <param name="text">String to be Checked</param>
        /// <param name="toLower">String in LowerCase</param>
        /// <returns>Returns bool on "if Contains String"</returns>
        public static bool Contains(
            this IEnumerable<string> enumerable, 
            string text, 
            bool toLower = true)
        {
            bool contains = false;
            foreach (string s in enumerable)
            {
                string auxString = toLower ? s.ToLower() : s;
                contains |= auxString
                    .Contains(toLower ? text.ToLower() : text);
                if (contains)
                    break;
            }
            return contains;
        }

        /// <summary>
        /// Checks if Char Enumerable Contains String
        /// </summary>
        /// <param name="enumerable">Char Enumerable</param>
        /// <param name="text">String to be Checked</param>
        /// <param name="toLower">String in LowerCase</param>
        /// <returns>Returns bool on "if Contains String"</returns>
        public static bool Contains(
            this IEnumerable<IEnumerable<char>> enumerable, 
            string text, 
            bool toLower = true)
        {
            bool contains = false;
            foreach (IEnumerable<char> s in enumerable)
            {
                string auxString = toLower ? 
                    s.ConvertToString().ToLower() : 
                    s.ConvertToString();
                contains |= auxString.Contains(
                    toLower ? text.ToLower() : text);
                if (contains)
                    break;
            }
            return contains;
        }

        /// <summary>
        /// Sorts an Enumerable of "Person"
        /// </summary>
        /// <typeparam name="TKey">Type to be Sorted</typeparam>
        /// <param name="enumerable">Enumerable of Person</param>
        /// <param name="keySelector">The key to be sorted by</param>
        /// <returns>Sorted Enumerable of Person</returns>
        public static IEnumerable<Person> Sort<TKey>(
            this IEnumerable<Person> enumerable, 
            Func<Person, TKey> keySelector) => enumerable.OrderBy(keySelector);
        
        /// <summary>
        /// Prints Person PrimaryNames
        /// </summary>
        /// <param name="people">Enumerable of Person</param>
        /// <param name="foreColor">Foreground Color</param>
        public static void Print(
            this IEnumerable<Person> people, 
            ConsoleColor foreColor = ConsoleColor.White)
        {
            people.Select(x => x.PrimaryName).ToStringArray().Print(
                foreColor);
        }

        /// <summary>
        /// Prints Enumerable of Char in String Array
        /// </summary>
        /// <param name="enumerable">Enumerable of Char Enumerable</param>
        /// <param name="foreColor">Foreground Color</param>
        /// <param name="backColor">Background Color</param>
        /// <param name="changeLine">Whether it should change Line at the 
        /// End</param>
        public static void Print(
          this IEnumerable<IEnumerable<char>> enumerable,
          ConsoleColor foreColor,
          ConsoleColor backColor = ConsoleColor.Black, 
          bool changeLine = true)
        {
            int count = enumerable.Count();
            int counter = 0;
            foreach (IEnumerable<char> e in enumerable)
            {
                new string(e.ToArray()).Print(foreColor, backColor, false);
                if (counter < count - 1)
                    ", ".Print(foreColor, backColor, changeLine);
                else
                    "".Print(foreColor, backColor, changeLine);
                counter++;
            }
        }
    }
}