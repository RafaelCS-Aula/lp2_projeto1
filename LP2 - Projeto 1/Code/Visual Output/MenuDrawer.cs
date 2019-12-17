using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Draws the various menus the user will interact with
    /// </summary>
    sealed class MenuDrawer : GeneralRenderer, IMenu
    {
        ResultsPrinter rP;
        public MenuDrawer(ResultsPrinter resultPrt, IMDBSearcher searcher)
        {
            //Renderer = renderer;
            DrawTitle();
            DrawLoading();
            rP = resultPrt;
            IMDBSearcher = searcher;
            
        }

        private void DrawTitleSearchMenu(
            SearchType searchType)
        {
            List<string[]> options;
            IPrintable textBox;
            IDictionary<Title, Rating> titles;
            DrawTitle();
            switch (searchType)
            {
                case SearchType.Title:
                    textBox = new TextBox("Title", new Rect(35, 13, 50, 2));
                    textBox.Print();
                    string title = ((TextBox)textBox).Get();
                    DrawTitle();
                    DrawLoading();
                    titles =
                        IMDBSearcher.LoadTitles(SearchType.Title, title);
                    rP.PrintTitleResults(titles);
                    break;
                case SearchType.ForAdults:
                    DrawTitle();
                    options = new List<string[]>()
                    {
                        new string[] { "true" },
                        new string[] { "false" }
                    };
                    DrawMenu(
                        "Search Title By Adult Content",
                        new Rect(35, 10, 50, 4),
                        options,
                        (short e) =>
                        {
                            Console.Clear();
                            DrawTitle();
                            DrawLoading();
                            titles =
                                IMDBSearcher.LoadTitles(SearchType.ForAdults,
                                options[e][0]);

                            rP.PrintTitleResults(titles);
                            DrawTitle();
                            titles = null;
                            GC.Collect();
                            return false;
                        },
                        () => { });
                    break;
                case SearchType.StartYear:
                    textBox = new TextBox("Start Year", new Rect(35, 13, 50, 2));

                    DrawTitle();
                    textBox.Print();
                    string startYear = ((TextBox)textBox).Get();

                    DrawTitle();
                    DrawLoading();
                    titles =
                    IMDBSearcher.LoadTitles(SearchType.StartYear, startYear);

                    rP.PrintTitleResults(titles);
                    break;
                case SearchType.EndYear:
                    textBox = new TextBox("End Year", new Rect(35, 13, 50, 2));
                    DrawTitle();
                    textBox.Print();
                    string endYear = ((TextBox)textBox).Get();
                    DrawTitle();
                    DrawLoading();
                    titles =
                        IMDBSearcher.LoadTitles(SearchType.EndYear, endYear);
                    rP.PrintTitleResults(titles);
                    break;
                case SearchType.Type:
                    DrawTitle();
                    options = new List<string[]>();
                    foreach (string s in IMDBSearcher.Types.ToArray())
                        options.Add(new string[] { s });
                    DrawMenu(
                        "Search Title By Type",
                        new Rect(35, 10, 50, IMDBSearcher.Types.Count + 1),
                        options,
                        (short e) =>
                        {
                            DrawTitle();
                            DrawLoading();
                            titles =
                                IMDBSearcher.LoadTitles(SearchType.Type,
                                IMDBSearcher.Types.ElementAt(e));
                            rP.PrintTitleResults(titles);
                            DrawTitle();
                            titles = null;
                            GC.Collect();
                            return false;
                        },
                        () => { });
                    break;
                case SearchType.Genre:
                    DrawTitle();
                    options = new List<string[]>();
                    foreach (string s in IMDBSearcher.Genres.ToArray())
                        options.Add(new string[] { s });
                DrawMenu(
                        "Search Title By Genre",
                        new Rect(35, 10, 50, IMDBSearcher.Genres.Count + 1),
                        options,
                        (short e) =>
                        {
                            Console.Clear();
                            DrawTitle();
                            Console.WriteLine("Loading...");
                            titles =
                                IMDBSearcher.LoadTitles(SearchType.Genre,
                                IMDBSearcher.Genres.ElementAt(e));
                            rP.PrintTitleResults(titles);
                            DrawTitle();
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
                    DrawTitle();
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
                    DrawTitle();
                    DrawLoading();
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
                    rP.PrintTitleResults(titles);
                    break;
            }
            DrawTitle();
            DrawLoading();
            titles = null;
            GC.Collect();
            DrawTitle();
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
                    DrawTitle();
                    textBox.Print();
                    string name = ((TextBox)textBox).Get();
                    DrawTitle();
                    DrawLoading();
                    people =
                        IMDBSearcher.LoadNames(SearchType.Name, name);
                    rP.PrintNameResults(people);
                    break;
                case SearchType.BirthYear:
                    textBox = new TextBox("Birth Year", new Rect(35, 13, 50, 2));
                    DrawTitle();
                    textBox.Print();
                    string birthYear = ((TextBox)textBox).Get();
                    DrawTitle();
                    DrawLoading();
                    people =
                        IMDBSearcher.LoadNames(SearchType.BirthYear, birthYear);
                    rP.PrintNameResults(people);
                    break;
                case SearchType.DeathYear:
                    textBox = new TextBox("Death Year", new Rect(35, 13, 50, 2));
                    DrawTitle();
                    textBox.Print();
                    string deathYear = ((TextBox)textBox).Get();
                    DrawTitle();
                    DrawLoading();
                    people =
                        IMDBSearcher.LoadNames(SearchType.DeathYear, deathYear);
                    rP.PrintNameResults(people);
                    break;
                case SearchType.Profession:
                    DrawTitle();
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

                    DrawMenu(
                        "Search Person By Profession",
                        new Rect(35, 10, 50, options.Count + 1),
                        options,
                        (short e) =>
                        {
                            DrawTitle();
                            Console.WriteLine("Loading...");
                            people =
                                IMDBSearcher.LoadNames(SearchType.Profession,
                                options[e][0].ToLower().Replace(" ", "_"));
                            rP.PrintNameResults(people);
                            DrawTitle();
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
                    DrawTitle();
                    textBox.Print();
                    textBox2.Print();
                    textBox3.Print();
                    textBox4.Print();
                    string name2 = ((TextBox)textBox).Get();
                    string birthYear2 = ((TextBox)textBox2).Get();
                    string deathYear2 = ((TextBox)textBox3).Get();
                    string profession2 = ((TextBox)textBox4).Get();
                    DrawTitle();
                    DrawLoading();
                    string query = string.Join(',', new string[]
                    {
                        name2,
                        birthYear2,
                        deathYear2,
                        profession2,
                    });
                    people =
                        IMDBSearcher.LoadNames(SearchType.Custom, query);
                    rP.PrintNameResults(people);
                    break;
            }
            DrawTitle();
            DrawLoading();
            people = null;
            GC.Collect();
            DrawTitle();
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

            DrawMenu(
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

                    DrawTitle();

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

            DrawMenu(
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

                    DrawTitle();

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

            DrawMenu(
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

                    DrawTitle();

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