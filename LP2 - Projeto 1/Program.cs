using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading;

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

            IMenuRenderer menuRenderer 
                = new MenuRenderer();
            IMenu menu = new Menu(
                menuRenderer, 
                appName);
            menu.MainLoop();

            Console.Clear();
        }
    }
}
