using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LP2___Projeto_1
{
    public static class EnumerableExtensions
    {
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

        public static bool Contains(
            this IEnumerable<string> enumerable, 
            string text, 
            bool toLower = true)
        {
            bool contains = false;
            foreach (string s in enumerable)
            {
                string auxString = toLower ? s.ToLower() : s;
                contains |= auxString.Contains(toLower ? text.ToLower() : text);
                if (contains)
                    break;
            }
            return contains;
        }

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

        public static IEnumerable<Person> Sort<TKey>(
            this IEnumerable<Person> enumerable, 
            Func<Person, TKey> keySelector)
        {
            return enumerable.OrderBy(keySelector);
        }

        public static void Print(
            this IEnumerable<Person> people, 
            ConsoleColor foreColor = ConsoleColor.White)
        {
            people.Select(x => x.PrimaryName).ToStringArray().Print(
                foreColor);
        }

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