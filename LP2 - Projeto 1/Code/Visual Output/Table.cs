using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for Table
    /// </summary>
    sealed public class Table : IPrintable
    {
        /// <summary>
        /// Class for Columns
        /// </summary>
        public class Column
        {
            public string Header { get; set; }
            public Rect Size { get; set; }

            /// <summary>
            /// Constructor, Sets Column Size 
            /// </summary>
            /// <param name="size">Size of Column</param>
            public Column(Rect size)
            {
                Size = size;
            }
        }

        private IEnumerable<IEnumerable<string>> options;

        /// <summary>
        /// Options in Table
        /// </summary>
        public IEnumerable<IEnumerable<string>> Options
        {
            get => options;

            set
            {
                options = value;
                if (options == null || options.Count() == 0)
                {
                    Columns = new Column[0];
                    return;
                }
                Columns = new Column[Options.ElementAt(0).Count()];
                int i = 0;
                foreach (string s in Options.ElementAt(0))
                {
                    Columns[i] = new Column(new Rect(0, 0, 50, 1));
                    i++;
                }
            }
        }

        /// <summary>
        /// Table Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Table Dimensions
        /// </summary>
        public Rect Size { get; }

        /// <summary>
        /// Table Columns
        /// </summary>
        public Column[] Columns { get; private set; }

        /// <summary>
        /// Table Current Selection
        /// </summary>
        public int Selection { get; set; }

        /// <summary>
        /// Alternate Color
        /// </summary>
        public bool Interleave { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="options">Table Options</param>
        /// <param name="size">Table Size</param>
        /// <param name="interleave">Alternate Color</param>
        public Table(
            string title, 
            IEnumerable<IEnumerable<string>> options, 
            Rect size,
            bool interleave = false)
        {
            Title = title;
            Options = options;
            Size = size;
            Interleave = interleave;
        }

        /// <summary>
        /// Clears Table
        /// </summary>
        public void Clear()
        {
            Console.CursorLeft = Size.X + (Size.Width / 2) - Title.Length / 2;
            Console.CursorTop = Size.Y;
            ' '.Repeat(Title.Length).Print(ConsoleColor.Black);
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < Size.Height - 1; i++)
            {
                Console.CursorLeft = Size.X;
                ' '.Repeat(Size.X + Size.Width).Print(ConsoleColor.Black);
            }
        }

        /// <summary>
        /// Prints Table
        /// </summary>
        public void Print()
        {
            Console.CursorLeft = Size.X + (Size.Width / 2) - Title.Length / 2;
            Console.CursorTop = Size.Y;
            Title.Print(ConsoleColor.White);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Red;
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

            Console.ForegroundColor = ConsoleColor.Black;
            int tempColumnX = Size.X + 5;
            int columnCounter = 0;
            foreach (Column c in Columns)
            {
                Console.CursorTop = Size.Y + 2;
                if (columnCounter > 0)
                    tempColumnX += 
                        Columns[columnCounter - 1]
                        .Size.Width;
                Console.CursorLeft = tempColumnX;
                c.Header.Print(ConsoleColor.Black, 
                               ConsoleColor.Red, 
                               false);
                columnCounter++;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (Options != null)
            {
                int tempY = Size.Y + 3;
                int counter = 0;
                foreach (string[] o in Options)
                {
                    int counter2 = 0;
                    int tempX = Size.X + 5;
                    foreach (string s in o)
                    {
                        Console.CursorTop = tempY;
                        if (counter2 > 0 && Columns.Length > 0)
                            tempX += 
                                Columns[counter2 - 1].Size.Width;
                        Console.CursorLeft = tempX;
                        s.Print(
                            Selection == counter ? 
                                ConsoleColor.Black :
                                Interleave ? 
                                    ((counter % 2) == 0 ?
                                        ConsoleColor.DarkGray :
                                        ConsoleColor.White) :
                                    ConsoleColor.White,
                            Selection == counter ? 
                                ConsoleColor.White : 
                                ConsoleColor.Black, 
                            false);
                        counter2++;
                    }
                    counter++;
                    tempY++;
                    
                }
            }
            Console.CursorLeft = 0;
            Console.CursorTop = Size.Y + Size.Height + 1;
        }
    }
}
