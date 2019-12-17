using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Works do Display the various menus onscreen
    /// </summary>
    public abstract class GeneralRenderer : IMenuRenderer
    {
        protected  IMDBSearcher IMDBSearcher { get;  set; }
       
        public int MaxResults { get; set; }

        protected virtual string Figlet => @"
'####:'##::::'##:'########::'########:::'######::'########::::'###::::'########:::'######::'##::::'##:'########:'########::
. ##:: ###::'###: ##.... ##: ##.... ##:'##... ##: ##.....::::'## ##::: ##.... ##:'##... ##: ##:::: ##: ##.....:: ##.... ##:
: ##:: ####'####: ##:::: ##: ##:::: ##: ##:::..:: ##::::::::'##:. ##:: ##:::: ##: ##:::..:: ##:::: ##: ##::::::: ##:::: ##:
: ##:: ## ### ##: ##:::: ##: ########::. ######:: ######:::'##:::. ##: ########:: ##::::::: #########: ######::: ########::
: ##:: ##. #: ##: ##:::: ##: ##.... ##::..... ##: ##...:::: #########: ##.. ##::: ##::::::: ##.... ##: ##...:::: ##.. ##:::
: ##:: ##:.:: ##: ##:::: ##: ##:::: ##:'##::: ##: ##::::::: ##.... ##: ##::. ##:: ##::: ##: ##:::: ##: ##::::::: ##::. ##::
'####: ##:::: ##: ########:: ########::. ######:: ########: ##:::: ##: ##:::. ##:. ######:: ##:::: ##: ########: ##:::. ##:
....::..:::::..::........:::........::::......:::........::..:::::..::..:::::..:::......:::..:::::..::........::..:::::..::";


        public void DrawTitle(
            ConsoleColor color = ConsoleColor.Red)
        {
            Console.Clear();

            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(Figlet);
            Console.ForegroundColor = tempColor;
        }

        public void DrawLoading()
        {
            Console.WriteLine();
            Console.WriteLine("Loading...");
        }

        public void DrawMenu(
           string title,
           Rect size,
           IEnumerable<string[]> options,
           Func<short, bool> onEnter,
           Action onEscape)
        {
            DrawTitle();
            IPrintable t = new Table(title, options, size);
            short selection = 0;
            bool goBack = false;
            while (!goBack)
            {
                ((Table)t).Selection = selection;

                t.Print();

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        goBack = onEnter.Invoke(selection);
                        break;
                    case ConsoleKey.Escape:
                        onEscape.Invoke();
                        goBack = true;
                        break;
                    case ConsoleKey.UpArrow:
                        selection--;
                        break;
                    case ConsoleKey.DownArrow:
                        selection++;
                        break;
                }

                if (selection < 0)
                    selection = (short)(options.Count() - 1);
                else if (selection > options.Count() - 1)
                    selection = 0;

                ((Table)t).Clear();
            }
        }

        public void DrawResults(
            Action<IPrintable, int, int> onIteration,
            Func<IPrintable, int, int, int> onDraw,
            Action<ConsoleKeyInfo, int> onKeyPress,
            int totalElements,
            string title
            )
        {
            DrawTitle();

            if (totalElements == 0)
            {
                Console.CursorLeft = 35;
                Console.CursorTop = 15;

                Console.WriteLine("No Results...Press Enter To Go Back");
                Console.ReadLine();
                return;
            }
            uint maxPages =
               (uint)Math.Ceiling((float)totalElements / MaxResults);
            int currentPage = 0;
            int selection = 0;

            IPrintable table = new Table(
                title + " 0/0",
                null,
                new Rect(0, 13, 123, 16),
                true);

            bool goBack = false;
            while (!goBack)
            {
                ((Table)table).Clear();

                ((Table)table).Title = title + " " +
                    (currentPage + 1) +
                    "/" + maxPages.ToString();

                int index;
                for (index = currentPage * MaxResults;
                        index < Math.Min(
                            currentPage * MaxResults + MaxResults,
                            totalElements);
                        index++)
                    onIteration?.Invoke(table, index, selection);

                int counter = onDraw.Invoke(table, selection, MaxResults);

                ConsoleKeyInfo a = Console.ReadKey();
                onKeyPress?.Invoke(a, selection);
                if (a.Key == ConsoleKey.UpArrow)
                {
                    selection--;
                    if (selection < currentPage * MaxResults)
                        selection = currentPage * MaxResults + counter - 1;
                }
                else if (a.Key == ConsoleKey.DownArrow)
                {
                    selection++;
                    if (selection >= currentPage * MaxResults + counter)
                        selection = currentPage * MaxResults;
                }
                else if (a.Key == ConsoleKey.Escape)
                    goBack = true;
                else if (a.Key == ConsoleKey.LeftArrow)
                {
                    currentPage--;
                    if (currentPage < 0)
                    {
                        currentPage = (int)maxPages - 1;
                        selection += MaxResults * (currentPage);
                        selection = Math.Min(selection, totalElements - 1);
                    }
                    else
                    {
                        selection -= MaxResults;
                        selection = Math.Max(selection, 0);
                    }
                }
                else if (a.Key == ConsoleKey.RightArrow)
                {
                    currentPage++;
                    if (currentPage >= maxPages)
                    {
                        selection -= MaxResults * (currentPage - 1);
                        selection = Math.Max(selection, 0);
                        currentPage = 0;
                    }
                    else
                    {
                        selection += MaxResults;
                        selection = Math.Min(selection, totalElements - 1);
                    }
                }
            }
        }

        public void DrawTitleSpecs(
            KeyValuePair<Title, Rating> title,
            IDictionary<Person, Principal> cast,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers)
        {
            DrawTitle();
            Console.WriteLine();

            "Title : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
            title.Key.ToString().Print(ConsoleColor.White);
            Console.WriteLine();
            title.Value?.Print();
            Console.WriteLine();

            "Director(s) : ".Print(
                    ConsoleColor.Red,
                    ConsoleColor.Black,
                    false);
            directors.Print(ConsoleColor.White);

            "Writer(s) : ".Print(
                    ConsoleColor.Red,
                    ConsoleColor.Black,
                    false);
            writers.Print(ConsoleColor.White);

            Console.WriteLine();
            cast.Print(ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine();

            "Press [Enter] To Continue...".Print(ConsoleColor.Red);

            Console.ReadLine();
        }

        public void DrawEpisodeSpecs(
            KeyValuePair<string, Episode> episode,
            KeyValuePair<Title, Rating> title,
            IDictionary<Person, Principal> cast,
            IEnumerable<Person> directors,
            IEnumerable<Person> writers)
        {
            DrawTitle();
            Console.WriteLine();

            "Title : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
            title.Key.ToString().Print(ConsoleColor.White);
            Console.WriteLine();
            title.Value?.Print();
            Console.WriteLine();

            "Episode : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
            episode.ToString().Print(ConsoleColor.White);
            Console.WriteLine();

            "Director(s) : ".Print(
                    ConsoleColor.Red,
                    ConsoleColor.Black,
                    false);
            directors.Print(ConsoleColor.White);

            "Writer(s) : ".Print(
                    ConsoleColor.Red,
                    ConsoleColor.Black,
                    false);
            writers.Print(ConsoleColor.White);

            Console.WriteLine();
            cast.Print(ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine();

            "Press [Enter] To Continue...".Print(ConsoleColor.Red);

            Console.ReadLine();
        }

        public void DrawTitleSpecs(
           KeyValuePair<Title, Rating> title,
           KeyValuePair<Title, Rating>? episodeParent,
           IDictionary<Person, Principal> cast,
           IEnumerable<Person> directors,
           IEnumerable<Person> writers)
        {
            DrawTitle();
            Console.WriteLine();

            "Title : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
            title.Key.ToString().Print(ConsoleColor.White);
            Console.WriteLine();
            title.Value?.Print();
            Console.WriteLine();

            if (episodeParent.HasValue)
            {
                "Series : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
                episodeParent.Value.Key.ToString().Print(ConsoleColor.White);
                Console.WriteLine();
                episodeParent.Value.Value?.Print();
                Console.WriteLine();
            }

            "Director(s) : ".Print(
                    ConsoleColor.Red,
                    ConsoleColor.Black,
                    false);
            directors.Print(ConsoleColor.White);

            "Writer(s) : ".Print(
                    ConsoleColor.Red,
                    ConsoleColor.Black,
                    false);
            writers.Print(ConsoleColor.White);

            Console.WriteLine();
            cast.Print(ConsoleColor.White);

            Console.WriteLine();
            Console.WriteLine();

            "Press [Enter] To Continue...".Print(ConsoleColor.Red);

            Console.ReadLine();
        }

        public void DrawNameSpecs(
            Person person,
            IDictionary<string, Title> knownForTitles)
        {
            DrawTitle();

            Console.WriteLine();
            person.Print();
            Console.WriteLine();
            "Known For Titles : ".Print(ConsoleColor.Yellow);
            if (knownForTitles != null)
                knownForTitles.Print(ConsoleColor.White);

            Console.WriteLine();
            "Press [Enter] To Continue...".Print(ConsoleColor.Red);

            Console.ReadLine();
        }
    }
}