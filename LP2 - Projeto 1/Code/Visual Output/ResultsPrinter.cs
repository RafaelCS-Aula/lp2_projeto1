using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Responsible for printing the results of searches
    /// </summary>
    sealed class ResultsPrinter : GeneralRenderer
    {
        public ResultsPrinter(IMDBSearcher searcher)
        {
            IMDBSearcher = searcher;
        }

        public void PrintTitleResults(
            IDictionary<Title, Rating> filteredTitles)
        {
            List<KeyValuePair<Title, Rating>> titles = filteredTitles.ToList();
            List<string[]> options = new List<string[]>();

            DrawResults(
                onIteration:(IPrintable table, int index, int selection) =>
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
                onDraw:(IPrintable table, int selection, int maxResults) =>
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
                onKeyPress:(ConsoleKeyInfo keyInfo, int selection) =>
                {
                    if (keyInfo.Key == ConsoleKey.A)
                        titles = titles.Sort(x => x.Key.PrimaryTitle.ConvertToString());
                    else if (keyInfo.Key == ConsoleKey.S)
                        titles = titles.Sort(x => x.Key.StartYear);
                    else if (keyInfo.Key == ConsoleKey.D)
                        titles = titles.Sort(x => x.Value?.AvarageRating);
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        DrawTitle();

                        Console.WriteLine();
                        Console.WriteLine("Loading...");
                        PrintTitleSpecs(titles[selection]);

                        DrawTitle();
                    }
                },
                totalElements:titles.Count,
                title:"Title Result"
                );
        }
        
        private void PrintEpisodeResults(
           IDictionary<string, Episode> filteredEpisodes,
           KeyValuePair<Title, Rating> title)
        {
            List<KeyValuePair<string, Episode>> episodes = filteredEpisodes.ToList();
            List<string[]> options = new List<string[]>();
            
                DrawResults(
                onIteration:(IPrintable table, int index, int selection) =>
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
                onDraw:(IPrintable table, int selection, int maxResults) =>
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
                onKeyPress:(ConsoleKeyInfo keyInfo, int selection) =>
                {
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        DrawTitle();
                        DrawLoading();

                        PrintEpisodeSpecs(episodes[selection], title);

                        DrawTitle();
                    }
                },
                totalElements:episodes.Count,
                title:"Episode Result"
                );
        }                

        public void PrintEpisodeSpecs(
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

                DrawEpisodeSpecs(
                episode,
                title,
                IMDBSearcher.LoadCast(people2, principals),
                IMDBSearcher.LoadDirectors(people2, crew),
                IMDBSearcher.LoadWriters(people2, crew));
        }
        
        public void PrintTitleSpecs(
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
                DrawTitleSpecs(
                    title,
                    episodeTitle,
                    IMDBSearcher.LoadCast(people2, principals),
                    IMDBSearcher.LoadDirectors(people2, crew),
                    IMDBSearcher.LoadWriters(people2, crew));
            }
            else
            {
                DrawTitleSpecs(
                    title,
                    IMDBSearcher.LoadCast(people2, principals),
                    IMDBSearcher.LoadDirectors(people2, crew),
                    IMDBSearcher.LoadWriters(people2, crew));
            }
        }

        public void PrintNameResults(
            Person[] people)
        {
            List<string[]> options = new List<string[]>();

            DrawResults(
                onIteration:(IPrintable table, int index, int selection) =>
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
                onDraw:(IPrintable table, int selection, int maxResults) =>
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
                onKeyPress:(ConsoleKeyInfo keyInfo, int selection) =>
                {
                    if (keyInfo.Key == ConsoleKey.A)
                        people = people.Sort(x => x.PrimaryName.ConvertToString()).ToArray();
                    else if (keyInfo.Key == ConsoleKey.S)
                        people = people.Sort(x => x.BirthYear).ToArray();
                    else if (keyInfo.Key == ConsoleKey.D)
                        people = people.Sort(x => x.DeathYear).ToArray();
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        DrawTitle();
                        DrawLoading();
                        PrintNameSpecs(people[selection]);
                        DrawTitle();
                    }
                },
                totalElements:people.Length,
                title:"People Result"
                );
        }

        public void PrintNameSpecs(
            Person person)
        {
            IDictionary<string, Title> values =
                IMDBSearcher.LoadTitles(person);

            DrawNameSpecs(
                person, 
                values);
        }

    }
}