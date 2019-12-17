using System;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Creates instances for Class IMDBSearcher with "MyIMDBSearcher", 
    /// for Class ResultsPrinter with IMDBSearcher instance and for Interface 
    /// IMenu with the previous IMDBSearcher and ResultsPrinter instances.
    /// </summary>
    sealed class Program
    {
        private const string appName = "MyIMDBSearcher";

        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(
                Console.LargestWindowWidth, 
                Console.LargestWindowHeight);

            IMDBSearcher searcher = new IMDBSearcher(appName);
            ResultsPrinter resultP = new ResultsPrinter(searcher);
            IMenu  menu = new MenuDrawer(resultP,searcher);
            menu.MainLoop();

            Console.Clear();
        }
    }
}
