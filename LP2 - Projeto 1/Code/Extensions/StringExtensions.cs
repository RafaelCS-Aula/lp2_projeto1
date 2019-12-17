using System;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Prints colored String in the Console
        /// </summary>
        /// <param name="s">Enumerable of String Enumerable</param>
        /// <param name="foreColor">Foreground Color</param>
        /// <param name="backColor">Background Color</param>
        /// <param name="changeLine">Whether it should change Line at the 
        /// End</param>
        public static void Print(
            this string s, 
            ConsoleColor foreColor = ConsoleColor.White, 
            ConsoleColor backColor = ConsoleColor.Black,
            bool changeLine = true)
        {
            ConsoleColor f = Console.ForegroundColor;
            ConsoleColor b = Console.BackgroundColor;
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            if (changeLine)
                Console.WriteLine(s);
            else
                Console.Write(s);
            Console.ForegroundColor = f;
            Console.BackgroundColor = b;
        }
    }
}
