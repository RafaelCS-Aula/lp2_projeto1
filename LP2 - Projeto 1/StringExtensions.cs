using System;
using System.Collections.Generic;
using System.Text;

namespace LP2___Projeto_1
{
    public static class StringExtensions
    {
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
