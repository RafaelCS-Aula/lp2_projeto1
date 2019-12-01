using System;

namespace Projeto_imdb_analyser
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instances
            // For text
            Render render = new Render();
            // For checking the validity of user's input
            Census census = new Census();
            // For using user's input
            React react = new React(); 

            bool keepUsing = true;
            
            // Main Loop
            while (keepUsing) 
            { 
                render.Introduction();
                render.ChooseSearch();

                int typeAnswer = census.SearchControlS();

                // Search Titles
                if (typeAnswer == 1)
                {
                    bool searchingTitle = true;

                    while (searchingTitle)
                    {
                        render.TitleText();
                        string titleAnswer = census.SearchControlT();

                        if (titleAnswer == "_EXIT_")
                        {
                            searchingTitle = false;
                        }
                        else
                        {    
                            react.ShowTitles(titleAnswer);
                        }
                    }
                }

                // Exit 'IMDB Analizer'
                else if (typeAnswer == 2)
                {
                    keepUsing = false;
                }
            }

            Environment.Exit;
        }
    }
}
