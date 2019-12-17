using System;


namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for ProgressBar
    /// </summary>
    public class ProgressBar : IPrintable
    {
        private float progress;

        public Rect Size { get; }
        public int PrintProgress { get; private set; }
        public float Progress
        {
            get => progress;

            set
            {
                progress = Math.Min(value, 1);
                if (progress > (float)(
                    (float)PrintProgress / (float)Size.Width))
                    Print();
            }
        }

        /// <summary>
        /// Constructor, Sets ProgressBar Size
        /// </summary>
        /// <param name="size">ProgressBar Size</param>
        public ProgressBar(Rect size)
        {
            Size = size;
        }

        /// <summary>
        /// Prints ProgressBar
        /// </summary>
        public void Print()
        {
            Console.CursorLeft = Size.X;
            Console.CursorTop = Size.Y;
            PrintProgress = 0;
            for (int i = 0; i < Size.Width; i++)
            {
                Console.CursorLeft = Size.X + i;
                if (progress > (float)((float)i / (float)Size.Width))
                {
                    Console.Write("#");
                    PrintProgress++;
                }
                else
                    Console.Write("-");
            }
            Console.CursorLeft = Size.X;
            Console.Write("[");
            Console.CursorLeft = Size.X + Size.Width;
            Console.Write("]");
            Console.CursorLeft = Size.X + Size.Width + 1;
            Console.Write((Progress * 100).ToString("f0") + "%");
        }
    }
}
