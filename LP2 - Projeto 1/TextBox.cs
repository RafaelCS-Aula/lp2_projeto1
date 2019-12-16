using System;
using System.Collections.Generic;
using System.Text;

namespace LP2___Projeto_1
{
    sealed public class TextBox : IPrintable
    {
        public string Label { get; }
        public Rect Size { get; }

        public TextBox(string label, Rect size)
        {
            Label = label;
            Size = size;
        }

        public void Print()
        {
            Console.CursorLeft = Size.X - Label.Length - 1;
            Console.CursorTop = Size.Y + 1;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Label);
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorTop = Size.Y;
            Console.CursorLeft = Size.X;
            for (int i = 0; i < Size.Width; i++)
                Console.Write("¤");
            Console.WriteLine("");
            for (int i = 0; i < Size.Height - 1; i++)
            {
                Console.CursorLeft = Size.X;
                Console.Write("¤");
                Console.CursorLeft = Size.X + Size.Width - 1;
                Console.WriteLine("¤");
            }
            Console.CursorLeft = Size.X;
            for (int i = 0; i < Size.Width; i++)
                Console.Write("¤");

            Console.CursorLeft = Size.X + 2;
            Console.CursorTop = Size.Y + 1;
        }

        public string Get()
        {
            Console.CursorTop = Size.Y + 1;
            Console.CursorLeft = Size.X + 2;
            return Console.ReadLine();
        }
    }
}
