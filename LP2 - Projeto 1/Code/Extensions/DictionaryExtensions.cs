using System;
using System.Collections.Generic;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for Dictionary Extensions
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Prints Principal
        /// </summary>
        /// <param name="people">Principal People Dictionary</param>
        /// <param name="foreColor">Foreground Color</param>
        /// <param name="backColor">Background Color</param>
        public static void Print(
           this IDictionary<Person, Principal> people,
           ConsoleColor foreColor,
           ConsoleColor backColor = ConsoleColor.Black)
        {
            foreach (KeyValuePair<Person, Principal> person in people)
            {
                string name = person.Key.PrimaryName;
                if (name.Length > 27)
                    name = name.Substring(0, 27) + "...";
                name.Print(ConsoleColor.White, 
                           ConsoleColor.Black, 
                           false);
                Console.CursorLeft = 30;
                person.Key.BirthYear.ToString()
                          .Print(foreColor, 
                                 backColor, 
                                 false);
                Console.CursorLeft = 38;
                person.Value.Category.Print(
                        ConsoleColor.White, 
                        ConsoleColor.Black, 
                        false);
                Console.CursorLeft = 58;

                string job = person.Value.Job;
                if (job.Length > 30)
                    job = job.Substring(0, 32) + "...";
                if (job != @"\N")
                    job.Print(foreColor,
                              backColor,
                              false);

                Console.CursorLeft = 90;
                if (person.Value.Characters != @"\N")
                    person.Value.Characters.Print(
                            foreColor, 
                            backColor, 
                            false);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints Titles
        /// </summary>
        /// <param name="titles">Title Dictionary</param>
        /// <param name="foreColor">Foreground Color</param>
        /// <param name="backColor">Background Color</param>
        public static void Print(
           this IDictionary<string, Title> titles,
           ConsoleColor foreColor,
           ConsoleColor backColor = ConsoleColor.Black)
        {
            foreach (KeyValuePair<string, Title> title in titles)
            {
                string name = title.Value.PrimaryTitle;
                if (name.Length > 48)
                    name = name.Substring(0, 45) + "...";
                name.Print(foreColor, backColor, false);
                Console.CursorLeft = 48;
                title.Value.StartYear.ToString()
                    .Print(foreColor, backColor, false);
                Console.CursorLeft = 58;
                title.Value.Type.Print(foreColor, backColor, false);
                Console.CursorLeft = 80;
                title.Value.Genres.Print(foreColor, backColor, false);
                "".Print();
            }
        }
    }
}
