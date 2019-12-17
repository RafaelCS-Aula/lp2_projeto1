using System;

namespace LP2___Projeto_1
{
    sealed class Program
    {
        private const string appName = "MyIMDBSearcher";

        static void Main(string[] args)
        {
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(
                Console.LargestWindowWidth, 
                Console.LargestWindowHeight);

            IMDBSearcher searcher = new IMDBSearcher("My IMDB Searcher");
            ResultsPrinter resultP = new ResultsPrinter(searcher);
            IMenu  menu = new MenuDrawer(resultP,searcher);
            menu.MainLoop();

            Console.Clear();
        }
    }
}
