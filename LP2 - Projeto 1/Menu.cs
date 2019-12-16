using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.IO.Compression;

namespace LP2___Projeto_1
{
    public interface IMenu
    {
        void MainLoop();
    }

    sealed public class Menu : IMenu
    {
        private IMDBSearcher IMDBSearcher { get; }
        private IMenuRenderer Renderer { get; }

        public Menu(
            IMenuRenderer renderer, 
            string appName)
        {
            Renderer = renderer;
            Renderer.DrawTitle();
            Renderer.DrawLoading();
            IMDBSearcher = new IMDBSearcher(appName);
        }

        private void DrawTitleSearchMenu(
            SearchType searchType)
        {
            List<string[]> options;
            IPrintable textBox;
            IDictionary<Title, Rating> titles;
            Renderer.DrawTitle();
            switch (searchType)
            {
                case SearchType.Title:
                    textBox = new TextBox("Title", new Rect(35, 13, 50, 2));
                    textBox.Print();
                    string title = ((TextBox)textBox).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    titles =
                        IMDBSearcher.LoadTitles(SearchType.Title, title);
                    PrintTitleResults(titles);
                    break;
                case SearchType.ForAdults:
                    Renderer.DrawTitle();
                    options = new List<string[]>()
                    {
                        new string[] { "true" },
                        new string[] { "false" }
                    };
                    Renderer.DrawMenu(
                        "Search Title By Adult Content",
                        new Rect(35, 10, 50, 4),
                        options,
                        (short e) =>
                        {
                            Console.Clear();
                            Renderer.DrawTitle();
                            Renderer.DrawLoading();
                            titles =
                                IMDBSearcher.LoadTitles(SearchType.ForAdults,
                                options[e][0]);
                            PrintTitleResults(titles);
                            Renderer.DrawTitle();
                            titles = null;
                            GC.Collect();
                            return false;
                        },
                        () => { });
                    break;
                case SearchType.StartYear:
                    textBox = new TextBox("Start Year", new Rect(35, 13, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    string startYear = ((TextBox)textBox).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    titles =
                        IMDBSearcher.LoadTitles(SearchType.StartYear, startYear);
                    PrintTitleResults(titles);
                    break;
                case SearchType.EndYear:
                    textBox = new TextBox("End Year", new Rect(35, 13, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    string endYear = ((TextBox)textBox).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    titles =
                        IMDBSearcher.LoadTitles(SearchType.EndYear, endYear);
                    PrintTitleResults(titles);
                    break;
                case SearchType.Type:
                    Renderer.DrawTitle();
                    options = new List<string[]>();
                    foreach (string s in IMDBSearcher.Types.ToArray())
                        options.Add(new string[] { s });
                    Renderer.DrawMenu(
                        "Search Title By Type",
                        new Rect(35, 10, 50, IMDBSearcher.Types.Count + 1),
                        options,
                        (short e) =>
                        {
                            Renderer.DrawTitle();
                            Renderer.DrawLoading();
                            titles =
                                IMDBSearcher.LoadTitles(SearchType.Type,
                                IMDBSearcher.Types.ElementAt(e));
                            PrintTitleResults(titles);
                            Renderer.DrawTitle();
                            titles = null;
                            GC.Collect();
                            return false;
                        },
                        () => { });
                    break;
                case SearchType.Genre:
                    Renderer.DrawTitle();
                    options = new List<string[]>();
                    foreach (string s in IMDBSearcher.Genres.ToArray())
                        options.Add(new string[] { s });
                    Renderer.DrawMenu(
                        "Search Title By Genre",
                        new Rect(35, 10, 50, IMDBSearcher.Genres.Count + 1),
                        options,
                        (short e) =>
                        {
                            Console.Clear();
                            Renderer.DrawTitle();
                            Console.WriteLine("Loading...");
                            titles =
                                IMDBSearcher.LoadTitles(SearchType.Genre,
                                IMDBSearcher.Genres.ElementAt(e));
                            PrintTitleResults(titles);
                            Renderer.DrawTitle();
                            titles = null;
                            GC.Collect();
                            return false;
                        },
                        () => { });
                    break;
                case SearchType.Custom:
                    textBox = new TextBox("Primary Title", new Rect(35, 13, 50, 2));
                    IPrintable textBox2 = new TextBox("Start Year", new Rect(35, 16, 50, 2));
                    IPrintable textBox3 = new TextBox("End Year", new Rect(35, 19, 50, 2));
                    IPrintable textBox4 = new TextBox("Type", new Rect(35, 22, 50, 2));
                    IPrintable textBox5 = new TextBox("Genre", new Rect(35, 25, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    textBox2.Print();
                    textBox3.Print();
                    textBox4.Print();
                    textBox5.Print();
                    string title2 = ((TextBox)textBox).Get();
                    string startYear2 = ((TextBox)textBox2).Get();
                    string endYear2 = ((TextBox)textBox3).Get();
                    string type = ((TextBox)textBox4).Get();
                    string genre = ((TextBox)textBox5).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    string query = string.Join(',', new string[]
                    {
                        title2,
                        startYear2,
                        endYear2,
                        type,
                        genre
                    });
                    titles =
                        IMDBSearcher.LoadTitles(SearchType.Custom, query);
                    PrintTitleResults(titles);
                    break;
            }
            Renderer.DrawTitle();
            Renderer.DrawLoading();
            titles = null;
            GC.Collect();
            Renderer.DrawTitle();
        }

        private void PrintTitleResults(
            IDictionary<Title, Rating> filteredTitles)
        {
            List<KeyValuePair<Title, Rating>> titles = filteredTitles.ToList();
            List<string[]> options = new List<string[]>();
            Renderer.DrawResults(
                (IPrintable table, int index, int selection) =>
                {
                    KeyValuePair<Title, Rating> t = titles[index];

                    string name = t.Key.PrimaryTitle;
                    if (name.Length > 48)
                        name = name.Substring(0, 48) + "...";

                    string c = t.Value == null ?
                        "N/A" :
                        (t.Value.AvarageRating.HasValue ?
                            t.Value
                            .AvarageRating.Value.ToString("f1") :
                            "N/A");

                    string s = t.Key.StartYear.HasValue ?
                        t.Key.StartYear.ToString() :
                        "N/A";

                    string g = t.Key.Genres.ToStringArray(3);
                    if (g.Length > 45)
                        g = g.Substring(0, 45) + "...";

                    options.Add(new string[] {
                            name,
                            c,
                            s,
                            g
                        });
                },
                (IPrintable table, int selection, int maxResults) =>
                {
                    ((Table)table).Options = options;
                    ((Table)table).Columns[0].Size = new Rect(0, 0, 55, 1);
                    ((Table)table).Columns[0].Header = "Primary Title";
                    ((Table)table).Columns[1].Size = new Rect(0, 0, 8, 1);
                    ((Table)table).Columns[1].Header = "Rating";
                    ((Table)table).Columns[2].Size = new Rect(0, 0, 8, 1);
                    ((Table)table).Columns[2].Header = "Year";
                    ((Table)table).Columns[3].Size = new Rect(0, 0, 20, 1);
                    ((Table)table).Columns[3].Header = "Genres";

                    ((Table)table).Selection = selection % maxResults;
                    table.Print();

                    int counter = options.Count;

                    options.Clear();

                    Console.CursorTop = 33;
                    Console.CursorLeft = 0;
                    for (int i = 33; i < 38; i++)
                        Console.WriteLine(' '.Repeat(Console.BufferWidth));

                    Console.CursorTop = 33;
                    "Selection : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
                    titles[selection].Key.ToString().Print();

                    Console.CursorTop = 35;
                    "Sorting : ".Print(ConsoleColor.Yellow, ConsoleColor.Black, false);
                    Console.WriteLine("a - By Name\ts - By Year\td - By Classification");

                    return counter;
                },
                (ConsoleKeyInfo keyInfo, int selection) =>
                {
                    if (keyInfo.Key == ConsoleKey.A)
                        titles = titles.Sort(x => x.Key.PrimaryTitle.ConvertToString());
                    else if (keyInfo.Key == ConsoleKey.S)
                        titles = titles.Sort(x => x.Key.StartYear);
                    else if (keyInfo.Key == ConsoleKey.D)
                        titles = titles.Sort(x => x.Value?.AvarageRating);
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Renderer.DrawTitle();

                        Console.WriteLine();
                        Console.WriteLine("Loading...");
                        PrintTitleSpecs(titles[selection]);

                        Renderer.DrawTitle();
                    }
                },
                titles.Count,
                "Title Result"
                );
        }

        private void PrintEpisodeResults(
           IDictionary<string, Episode> filteredEpisodes,
           KeyValuePair<Title, Rating> title)
        {
            List<KeyValuePair<string, Episode>> episodes = filteredEpisodes.ToList();
            List<string[]> options = new List<string[]>();
            Renderer.DrawResults(
                (IPrintable table, int index, int selection) =>
                {
                    KeyValuePair<string, Episode> t = episodes[index];

                    string name = "Episode " + t.Value.Number;
                    if (name.Length > 48)
                        name = name.Substring(0, 48) + "...";

                    string c = t.Value.Season;

                    options.Add(new string[] {
                            name,
                            c,
                        });
                },
                (IPrintable table, int selection, int maxResults) =>
                {
                    ((Table)table).Options = options;
                    ((Table)table).Columns[0].Size = new Rect(0, 0, 55, 1);
                    ((Table)table).Columns[0].Header = "Episode Nº";
                    ((Table)table).Columns[1].Size = new Rect(0, 0, 8, 1);
                    ((Table)table).Columns[1].Header = "Season";

                    ((Table)table).Selection = selection % maxResults;
                    table.Print();

                    int counter = options.Count;

                    options.Clear();

                    Console.CursorTop = 33;
                    Console.CursorLeft = 0;
                    for (int i = 33; i < 38; i++)
                        Console.WriteLine(' '.Repeat(Console.BufferWidth));

                    Console.CursorTop = 33;
                    "Selection : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
                    episodes[selection].Value.ToString().Print();

                    Console.CursorTop = 35;
                    "Sorting : ".Print(ConsoleColor.Yellow, ConsoleColor.Black, false);
                    Console.WriteLine("a - By Name\ts - By Year\td - By Classification");

                    return counter;
                },
                (ConsoleKeyInfo keyInfo, int selection) =>
                {
                    /*if (keyInfo.Key == ConsoleKey.A)
                        titles = titles.Sort(x => x.Key.PrimaryTitle.ConvertToString());
                    else if (keyInfo.Key == ConsoleKey.S)
                        titles = titles.Sort(x => x.Key.StartYear);
                    else if (keyInfo.Key == ConsoleKey.D)
                        titles = titles.Sort(x => x.Value?.AvarageRating);
                    else*/ if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Renderer.DrawTitle();
                        Renderer.DrawLoading();

                        PrintEpisodeSpecs(episodes[selection], title);

                        Renderer.DrawTitle();
                    }
                },
                episodes.Count,
                "Episode Result"
                );
        }

        private void PrintEpisodeSpecs(
            KeyValuePair<string, Episode> episode, 
            KeyValuePair<Title, Rating> title)
        {
            IEnumerable<IDictionary<string, IIMDBValue>> people =
              IMDBSearcher.LoadPeopleForTitle(title);
            IDictionary<string, Crew> crew =
                people.ElementAt(0).ToDictionary(t => t.Key, T => (Crew)T.Value);
            IDictionary<string, Principal> principals =
                people.ElementAt(1).ToDictionary(t => t.Key, T => (Principal)T.Value);
            IDictionary<string, Person> people2 =
                people.ElementAt(2).ToDictionary(t => t.Key, T => (Person)T.Value);

            Renderer.PrintEpisodeSpecs(
                episode,
                title,
                IMDBSearcher.LoadCast(people2, principals),
                IMDBSearcher.LoadDirectors(people2, crew),
                IMDBSearcher.LoadWriters(people2, crew));
        }

        private void PrintTitleSpecs(
           KeyValuePair<Title, Rating> title)
        {
            if (title.Key.IsSeries)
            {
                IDictionary<string, Episode> episodes =
                   IMDBSearcher.LoadEpisodes(title.Key);
                PrintEpisodeResults(episodes, title);

                return;
            }

            IEnumerable<IDictionary<string, IIMDBValue>> people =
               IMDBSearcher.LoadPeopleForTitle(title);
            IDictionary<string, Crew> crew =
                people.ElementAt(0).ToDictionary(t => t.Key, T => (Crew)T.Value);
            IDictionary<string, Principal> principals =
                people.ElementAt(1).ToDictionary(t => t.Key, T => (Principal)T.Value);
            IDictionary<string, Person> people2 =
                people.ElementAt(2).ToDictionary(t => t.Key, T => (Person)T.Value);

            if (title.Key.IsEpisode)
            {
                KeyValuePair<Title, Rating>? episodeTitle = IMDBSearcher.LoadEpisodeTitle(title.Key);
                Renderer.PrintTitleSpecs(
                    title,
                    episodeTitle,
                    IMDBSearcher.LoadCast(people2, principals),
                    IMDBSearcher.LoadDirectors(people2, crew),
                    IMDBSearcher.LoadWriters(people2, crew));
            }
            else
            {
                Renderer.PrintTitleSpecs(
                    title,
                    IMDBSearcher.LoadCast(people2, principals),
                    IMDBSearcher.LoadDirectors(people2, crew),
                    IMDBSearcher.LoadWriters(people2, crew));
            }
        }

        private void DrawPersonSearchMenu(
            SearchType searchType)
        {
            IPrintable textBox;
            List<string[]> options;
            Person[] people;
            switch (searchType)
            {
                case SearchType.Name:
                    textBox = new TextBox("Name", new Rect(35, 13, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    string name = ((TextBox)textBox).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    people =
                        IMDBSearcher.LoadNames(SearchType.Name, name);
                    PrintNameResults(people);
                    break;
                case SearchType.BirthYear:
                    textBox = new TextBox("Birth Year", new Rect(35, 13, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    string birthYear = ((TextBox)textBox).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    people =
                        IMDBSearcher.LoadNames(SearchType.BirthYear, birthYear);
                    PrintNameResults(people);
                    break;
                case SearchType.DeathYear:
                    textBox = new TextBox("Death Year", new Rect(35, 13, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    string deathYear = ((TextBox)textBox).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    people =
                        IMDBSearcher.LoadNames(SearchType.DeathYear, deathYear);
                    PrintNameResults(people);
                    break;
                case SearchType.Profession:
                    Renderer.DrawTitle();
                    options = new List<string[]>();
                    foreach (string s in IMDBSearcher.Professions.ToArray())
                        if (!string.IsNullOrEmpty(s))
                        {
                            string tempString
                                = s.First().ToString().ToUpper() +
                                s.Substring(1, s.Length - 1);
                            tempString = tempString.Replace("_", " ");
                            options.Add(new string[] { tempString });
                        }

                    Renderer.DrawMenu(
                        "Search Person By Profession",
                        new Rect(35, 10, 50, options.Count + 1),
                        options,
                        (short e) =>
                        {
                            Renderer.DrawTitle();
                            Console.WriteLine("Loading...");
                            people =
                                IMDBSearcher.LoadNames(SearchType.Profession,
                                options[e][0].ToLower().Replace(" ", "_"));
                            PrintNameResults(people);
                            Renderer.DrawTitle();
                            people = null;
                            GC.Collect();
                            return false;
                        },
                        () => { });
                    break;
                case SearchType.Custom:
                    textBox = new TextBox("Name", new Rect(35, 13, 50, 2));
                    IPrintable textBox2 = new TextBox("Birth Year", new Rect(35, 16, 50, 2));
                    IPrintable textBox3 = new TextBox("Death Year", new Rect(35, 19, 50, 2));
                    IPrintable textBox4 = new TextBox("Profession", new Rect(35, 22, 50, 2));
                    Renderer.DrawTitle();
                    textBox.Print();
                    textBox2.Print();
                    textBox3.Print();
                    textBox4.Print();
                    string name2 = ((TextBox)textBox).Get();
                    string birthYear2 = ((TextBox)textBox2).Get();
                    string deathYear2 = ((TextBox)textBox3).Get();
                    string profession2 = ((TextBox)textBox4).Get();
                    Renderer.DrawTitle();
                    Renderer.DrawLoading();
                    string query = string.Join(',', new string[]
                    {
                        name2,
                        birthYear2,
                        deathYear2,
                        profession2,
                    });
                    people =
                        IMDBSearcher.LoadNames(SearchType.Custom, query);
                    PrintNameResults(people);
                    break;
            }
            Renderer.DrawTitle();
            Renderer.DrawLoading();
            people = null;
            GC.Collect();
            Renderer.DrawTitle();
        }

        private void PrintNameResults(
            Person[] people)
        {
            List<string[]> options = new List<string[]>();
            Renderer.DrawResults(
                (IPrintable table, int index, int selection) =>
                {
                    Person t = people[index];

                    string name = t.PrimaryName.ConvertToString().ToString();
                    if (name.Length > 45)
                        name = name.Substring(0, 42) + "...";

                    string c = t.BirthYear.HasValue ?
                            t.BirthYear.Value.ToString() :
                            "N/A";

                    string s = t.DeathYear.HasValue ?
                        t.DeathYear.ToString() :
                        "N/A";

                    string[] p = t.PrimaryProfessions.ToStringArray();
                    for (int i = 0; i < p.Length; i++)
                        if (!string.IsNullOrEmpty(p[i]))
                            p[i] = p[i].First().ToString().ToUpper() +
                                   p[i].Substring(1, p[i].Length - 1);

                    string d = p.ToStringArray(3).Replace("_", " ");
                    if (d.Length > 45)
                        d = d.Substring(0, 45) + "...";

                    options.Add(new string[] {
                            name,
                            c,
                            s,
                            d
                        });
                },
                (IPrintable table, int selection, int maxResults) =>
                {
                    ((Table)table).Options = options;
                    ((Table)table).Columns[0].Size = new Rect(0, 0, 40, 1);
                    ((Table)table).Columns[0].Header = "Primary Name";
                    ((Table)table).Columns[1].Size = new Rect(0, 0, 12, 1);
                    ((Table)table).Columns[1].Header = "Birth Year";
                    ((Table)table).Columns[2].Size = new Rect(0, 0, 12, 1);
                    ((Table)table).Columns[2].Header = "Death Year";
                    ((Table)table).Columns[3].Size = new Rect(0, 0, 20, 1);
                    ((Table)table).Columns[3].Header = "Professions";

                    ((Table)table).Selection = selection % maxResults;
                    table.Print();

                    int counter = options.Count;

                    options.Clear();

                    Console.CursorTop = 33;
                    Console.CursorLeft = 0;
                    for (int i = 33; i < 38; i++)
                        Console.WriteLine(' '.Repeat(Console.BufferWidth));

                    Console.CursorTop = 33;
                    "Selection : ".Print(ConsoleColor.Red, ConsoleColor.Black, false);
                    people[selection].ToString().Print();

                    Console.CursorTop = 35;
                    "Sorting : ".Print(ConsoleColor.Yellow, ConsoleColor.Black, false);
                    Console.WriteLine("a - By Name\ts - By Birth Year\td - By Death Year");

                    return counter;
                },
                (ConsoleKeyInfo keyInfo, int selection) =>
                {
                    if (keyInfo.Key == ConsoleKey.A)
                        people = people.Sort(x => x.PrimaryName.ConvertToString()).ToArray();
                    else if (keyInfo.Key == ConsoleKey.S)
                        people = people.Sort(x => x.BirthYear).ToArray();
                    else if (keyInfo.Key == ConsoleKey.D)
                        people = people.Sort(x => x.DeathYear).ToArray();
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Renderer.DrawTitle();
                        Renderer.DrawLoading();
                        PrintNameSpecs(people[selection]);
                        Renderer.DrawTitle();
                    }
                },
                people.Length,
                "People Result"
                );
        }

        private void PrintNameSpecs(
            Person person)
        {
            IDictionary<string, Title> values =
                IMDBSearcher.LoadTitles(person);

            Renderer.PrintNameSpecs(
                person, 
                values);
        }

        private void DrawTitlesMenu()
        {
            string[] options = new string[]
                {
                    "Search by Primary Title",
                    "Search by Title type",
                    "Search for Adult Content",
                    "Search by Start Year",
                    "Search by End Year",
                    "Search by Genre",
                    "Custom"
                };

            List<string[]> myOptions = new List<string[]>();
            foreach (string s in options)
                myOptions.Add(new string[] { s });

            Renderer.DrawMenu(
                "Search Titles Menu",
                new Rect(35, 10, 50, 10),
                myOptions,
                (short e) =>
                {
                    switch (e)
                    {
                        case 0:
                            DrawTitleSearchMenu(SearchType.Title);
                            break;
                        case 1:
                            DrawTitleSearchMenu(SearchType.Type);
                            break;
                        case 2:
                            DrawTitleSearchMenu(SearchType.ForAdults);
                            break;
                        case 3:
                            DrawTitleSearchMenu(SearchType.StartYear);
                            break;
                        case 4:
                            DrawTitleSearchMenu(SearchType.EndYear);
                            break;
                        case 5:
                            DrawTitleSearchMenu(SearchType.Genre);
                            break;
                        case 6:
                            DrawTitleSearchMenu(SearchType.Custom);
                            break;
                    }

                    Renderer.DrawTitle();

                    return false;
                },
                () => { });
        }

        private void DrawPeopleMenu()
        {
            string[] options = new string[]
                {
                    "Search by Name",
                    "Search by Birth Year",
                    "Search by Death Year",
                    "Search by Profession",
                    "Custom"
                };

            List<string[]> myOptions = new List<string[]>();
            foreach (string s in options)
                myOptions.Add(new string[] { s });

            Renderer.DrawMenu(
                "Search People Menu",
                new Rect(35, 10, 50, 10),
                myOptions,
                (short e) =>
                {
                    switch (e)
                    {
                        case 0:
                            DrawPersonSearchMenu(SearchType.Name);
                            break;
                        case 1:
                            DrawPersonSearchMenu(SearchType.BirthYear);
                            break;
                        case 2:
                            DrawPersonSearchMenu(SearchType.DeathYear);
                            break;
                        case 3:
                            DrawPersonSearchMenu(SearchType.Profession);
                            break;
                        case 4:
                            DrawPersonSearchMenu(SearchType.Custom);
                            break;
                    }

                    Renderer.DrawTitle();

                    return false;
                },
                () => { });
        }

        private void DrawMainMenu()
        {
            string[] options = new string[]
                {
                    "Search for Titles",
                    "Search for People",
                    "Quit",
                };

            List<string[]> myOptions = new List<string[]>();
            foreach (string s in options)
                myOptions.Add(new string[] { s });

            Renderer.DrawMenu(
                "Main Menu",
                new Rect(35, 10, 50, 5),
                myOptions,
                (short e) =>
                {
                    switch (e)
                    {
                        case 0:
                            DrawTitlesMenu();
                            break;
                        case 1:
                            DrawPeopleMenu();
                            break;
                        case 2:
                            return true;
                    }

                    Renderer.DrawTitle();

                    return false;
                },
                () => { });
        }

        public void MainLoop()
        {
            DrawMainMenu();
        }
    }
}