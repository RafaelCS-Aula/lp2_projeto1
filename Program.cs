using System.Collections.Generic;
using System;

namespace Projeto_imdb_analyser
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var e = Enum.GetValues(typeof(GenreOptions));
        
            foreach (var t in e) Console.WriteLine(t);
            
            
            

        }
    }
}
