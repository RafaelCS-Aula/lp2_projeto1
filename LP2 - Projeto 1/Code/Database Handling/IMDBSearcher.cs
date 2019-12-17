using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.IO.Compression;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Has methods for searching database files
    /// </summary>
    public class IMDBSearcher
    {
        private readonly string fileTitleBasics     = 
            "title.basics.tsv.gz";
        private readonly string peopleFilename      = 
            "name.basics.tsv.gz";
        private readonly string crewFilename        = 
            "title.crew.tsv.gz";
        private readonly string ratingsFilename     = 
            "title.ratings.tsv.gz";
        private readonly string principalsFilename  = 
            "title.principals.tsv.gz";
        private readonly string episodesFilename    = 
            "title.episode.tsv.gz";

        // Database's Folder Name
        public string AppName { get; set; }

        /// <summary>
        /// Identifies Path for Folder with Database
        /// </summary>
        protected virtual string FolderWithFiles
        {
            get =>
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                       AppName);
        }
        protected virtual string FileTitleBasicsFull
            => Path.Combine(FolderWithFiles, fileTitleBasics);
        protected virtual string FilePeopleBasicsFull
            => Path.Combine(FolderWithFiles, peopleFilename);
        protected virtual string FileCrewsBasicsFull
            => Path.Combine(FolderWithFiles, crewFilename);
        protected virtual string FilePrincipalsBasicsFull
            => Path.Combine(FolderWithFiles, principalsFilename);
        protected virtual string FileRatingsBasicsFull
            => Path.Combine(FolderWithFiles, ratingsFilename);
        protected virtual string FileEpisodesBasicsFull
            => Path.Combine(FolderWithFiles, episodesFilename);
        protected int TitlesLineCount { get; set; }
        protected int RatingsLineCount { get; set; }
        protected int PeopleLineCount { get; set; }
        protected int CrewLineCount { get; set; }
        protected int PrincipalsLineCount { get; set; }
        protected int EpisodeLineCount { get; set; }
        public ISet<string> Genres { get; } = new HashSet<string>();
        public ISet<string> Types { get; } = new HashSet<string>();
        public ISet<string> Professions { get; } = new HashSet<string>();

        /// <summary>
        /// Starts App
        /// </summary>
        /// <param name="appName"> Database's Folder Name </param>
        public IMDBSearcher(string appName)
        {
            AppName = appName;
            LoadBasics();
        }

        /// <summary>
        /// Loads Episodes from Database
        /// </summary>
        /// <param name="title">Title in Database</param>
        /// <returns>Episodes from Database</returns>
        public virtual IDictionary<string, Episode> LoadEpisodes(
            Title title)
        {
            IIMDBReader<Episode> episodeReader = new IMDBFileReader<Episode>(
                FileEpisodesBasicsFull);

            if (EpisodeLineCount == 0)
                EpisodeLineCount = episodeReader.LineCount();

            int count = EpisodeLineCount;
            int progress = 0;
            IPrintable progressBar = new ProgressBar(new Rect(12, 10, 100, 1));
            progressBar.Print();

            return episodeReader.Read((int e) =>
                {
                    progress = e;
                    ((ProgressBar)progressBar).Progress =
                            (float)progress / (float)count;
                }).Where(x => x.ParentID.Contains(title.ID))
            .GroupBy(x => x.ID)
            .ToDictionary(t => t.Key, t => t.First());
        }

        /// <summary>
        /// Loads Titles and Ratings
        /// </summary>
        /// <param name="searchType">Search Type chosen by User</param>
        /// <param name="type">Title Type</param>
        /// <returns>Returns Titles from Database</returns>
        public virtual IDictionary<Title, Rating> LoadTitles(
           SearchType searchType,
           string type)
        {
            IIMDBReader<Title> titleReader
                = new IMDBFileReader<Title>(FileTitleBasicsFull);
            IDictionary<Title, Rating> values
                = new Dictionary<Title, Rating>();
            IIMDBReader<Rating> ratingsReader =
                new IMDBFileReader<Rating>(FileRatingsBasicsFull);

            if (TitlesLineCount == 0)
                TitlesLineCount = titleReader.LineCount();

            if (RatingsLineCount == 0)
                RatingsLineCount = ratingsReader.LineCount();

            int count = TitlesLineCount + RatingsLineCount;
            int progress = 0;
            IPrintable progressBar = new ProgressBar(new Rect(12, 10, 100, 1));
            progressBar.Print();
            try
            {
                switch (searchType)
                {
                    case SearchType.Episode:
                        IDictionary<string, Title> values0
                            = titleReader.Read((int e) =>
                        {
                            progress = e;
                            ((ProgressBar)progressBar).Progress =
                                    (float)progress / (float)count;
                        }).Where(x => x.ID.Contains(type))
                        .GroupBy(x => x.ID)
                        .ToDictionary(t => t.Key, t => t.First());
                        values = values0.GroupJoin(
                            ratingsReader.Read((int e) =>
                            {
                                ((ProgressBar)progressBar).Progress =
                                    (float)(progress + e) / (float)count;
                            }).Where(x => values0.
                            TryGetValue(x.ID, out Title t)),
                                    title => title.Key,
                                    rating => rating.ID,
                                    (t, r) => new { Title = t.Value, Rating = 
                                    r.Where(y => y.ID.Contains(t.Value.ID))
                                    .FirstOrDefault() })
                               .Where(x => x.Rating != null ? x.Rating.ID
                               .Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);
                        break;
                    case SearchType.Type:
                        IDictionary<string, Title> values1 =
                            titleReader.Read((int e) =>
                            {
                                progress = e;
                                ((ProgressBar)progressBar).Progress =
                                    (float)progress / (float)count;
                            })
                              .Where(x => x.Type.ToLower()
                              .Equals(type.ToLower()))
                              .GroupBy(x => x.ID)
                              .ToDictionary(t => t.Key, t => t.First());
                        values = values1
                              .GroupJoin(ratingsReader.Read((int e) =>
                              {
                                  ((ProgressBar)progressBar).Progress =
                                      (float)(progress + e) / (float)count;
                              }).Where(x => 
                              values1.TryGetValue(x.ID, out Title t)),
                                    title => title.Key,
                                    rating => rating.ID,
                                    (t, r) => new { Title = t.Value, Rating = 
                                    r.Where(y => y.ID.Contains(t.Value.ID))
                                    .FirstOrDefault() })
                               .Where(x => x.Rating != null ? x.Rating.ID
                               .Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);

                        break;

                    case SearchType.Title:
                        IDictionary<string, Title> values2 = 
                            titleReader.Read((int e) => {
                            progress = e;
                            ((ProgressBar)progressBar).Progress =
                                (float)progress / (float)count;
                        })
                                .Where(x => x.PrimaryTitle.ToLower()
                                .Contains(type.ToLower()))
                                .GroupBy(x => x.ID)
                                .ToDictionary(t => t.Key, t => t.First());
                        values = values2
                                .GroupJoin(ratingsReader.Read((int e) =>
                                {
                                    ((ProgressBar)progressBar).Progress =
                                        (float)(progress + e) / (float)count;
                                }).Where(x => values2.TryGetValue(
                                    x.ID, out Title t)),
                                    title => title.Key,
                                    rating => rating.ID,
                                    (t, r) => new { Title = t.Value, 
                                        Rating = r.Where(y => y.ID.Contains(
                                            t.Value.ID)).FirstOrDefault() })
                               .Where(x => x.Rating != null ? 
                               x.Rating.ID.Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);
                        break;

                    case SearchType.ForAdults:
                        string lower = type.ToLower();
                        bool isAdult = type.ToLower().Equals("true");
                        IDictionary<string, Title> values3 =
                            titleReader.Read((int e) =>
                            {
                                progress = e;
                                ((ProgressBar)progressBar).Progress =
                                    (float)progress / (float)count;
                            })
                                 .Where(x => x.IsAdult.Equals(isAdult))
                                 .GroupBy(x => x.ID)
                                 .ToDictionary(t => t.Key, t => t.First());
                        values = values3
                                 .GroupJoin(ratingsReader.Read((int e) =>
                                 {
                                     ((ProgressBar)progressBar).Progress =
                                        (float)(progress + e) / (float)count;
                                 }).Where(x => values3.TryGetValue(
                                     x.ID, out Title t)),
                                     title => title.Key,
                                     rating => rating.ID,
                                     (t, r) => new { Title = t.Value, 
                                         Rating = r.Where(y => 
                                         y.ID.Contains(t.Value.ID))
                                         .FirstOrDefault() })
                               .Where(x => x.Rating != null ? 
                               x.Rating.ID.Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);
                        break;

                    case SearchType.StartYear:
                        try
                        {
                            short.TryParse(type, out short startYear);

                            IDictionary<string, Title> values4 = 
                                titleReader.Read((int e) => {
                                progress = e;
                                ((ProgressBar)progressBar).Progress =
                                    (float)progress / (float)count;
                            })
                                .Where(x => x.StartYear.Equals(startYear))
                                .GroupBy(x => x.ID)
                                .ToDictionary(t => t.Key, t => t.First());
                            values = values4
                                .GroupJoin(ratingsReader.Read((int e) =>
                                {
                                    ((ProgressBar)progressBar).Progress =
                                        (float)(progress + e) / (float)count;
                                }).Where(x => values4.TryGetValue(
                                    x.ID, out Title t)),
                                    title => title.Key,
                                    rating => rating.ID,
                                     (t, r) => new { Title = t.Value, 
                                         Rating = r.Where(y => y.ID.Contains(
                                             t.Value.ID)).FirstOrDefault() })
                               .Where(x => x.Rating != null ?
                               x.Rating.ID.Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Year.");
                        }

                        break;
                    case SearchType.EndYear:
                        try
                        {
                            short.TryParse(type, out short endYear);

                            IDictionary<string, Title> values5 = 
                                titleReader.Read((int e) => {
                                progress = e;
                                ((ProgressBar)progressBar).Progress =
                                    (float)progress / (float)count;
                            })
                                .GroupBy(x => x.ID)
                                .ToDictionary(t => t.Key, t => t.First());
                            values = values5
                                .GroupJoin(ratingsReader.Read((int e) =>
                                {
                                    ((ProgressBar)progressBar).Progress =
                                        (float)(progress + e) / (float)count;
                                }).Where(x => values5.TryGetValue(
                                    x.ID, out Title t)),
                                    title => title.Key,
                                    rating => rating.ID,
                                    (t, r) => new { Title = t.Value, 
                                        Rating = r.Where(y => y.ID.Contains(
                                            t.Value.ID)).FirstOrDefault() })
                               .Where(x => x.Rating != null ? 
                               x.Rating.ID.Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid Year.");
                        }

                        break;
                    case SearchType.Genre:
                        IDictionary<string, Title> values6 = 
                            titleReader.Read((int e) => {
                            progress = e;
                            ((ProgressBar)progressBar).Progress =
                                (float)progress / (float)count;
                        })
                               .Where(x => x.Genres.Contains(type))
                               .GroupBy(x => x.ID)
                               .ToDictionary(t => t.Key, t => t.First());
                        values = values6
                                 .GroupJoin(ratingsReader.Read((int e) =>
                                 {
                                     ((ProgressBar)progressBar).Progress =
                                         (float)(progress + e) / (float)count;
                                 }).Where(x => values6.TryGetValue(
                                     x.ID, out Title t)),
                                    title => title.Key,
                                    rating => rating.ID,
                                    (t, r) => new { Title = t.Value, Rating = 
                                    r.Where(y => y.ID.Contains(t.Value.ID))
                                    .FirstOrDefault() })
                              .Where(x => x.Rating != null ? 
                              x.Rating.ID.Contains(x.Title.ID) :
                                           true)
                              .GroupBy(t => t.Title.ID)
                              .ToDictionary(t => t.FirstOrDefault()?.Title, t 
                              => t.FirstOrDefault()?.Rating);
                        break;
                    case SearchType.Custom:
                        string[] options = type.Split(',');
                        IDictionary<string, Title> values7 = 
                            titleReader.Read((int e) => {
                            progress = e;
                            ((ProgressBar)progressBar).Progress =
                                (float)progress / (float)count;
                        })
                            .Where(x => x.PrimaryTitle.ToLower()
                            .Contains(options[0].ToLower()) &&
                                        x.StartYear.ToString().ToLower()
                                        .Contains(options[1].ToLower()) &&
                                        x.EndYear.ToString().ToLower()
                                        .Contains(options[2].ToLower()) &&
                                        x.Type.ToLower()
                                        .Contains(options[3].ToLower()) &&
                                        x.Genres
                                        .Contains(options[4].ToLower(), true))
                            .GroupBy(x => x.ID)
                            .ToDictionary(t => t.Key, t => t.First());
                        values = values7
                            .GroupJoin(ratingsReader.Read((int e) =>
                            {
                                ((ProgressBar)progressBar).Progress =
                                    (float)(progress + e) / (float)count;
                            }).Where(x => values7.TryGetValue(
                                x.ID, out Title t)),
                                            title => title.Key,
                                            rating => rating.ID,
                                            (t, r) => new { Title = 
                                            t.Value, Rating = r.Where(y => 
                                            y.ID.Contains(t.Value.ID))
                                            .FirstOrDefault() })
                               .Where(x => x.Rating != null ? 
                               x.Rating.ID.Contains(x.Title.ID) :
                                           true)
                               .GroupBy(t => t.Title.ID)
                               .ToDictionary(t => t.FirstOrDefault()?.Title, 
                               t => t.FirstOrDefault()?.Rating);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }

            return values;
        }

        /// <summary>
        /// Loads Names (Persons)
        /// </summary>
        /// <param name="searchType">Search type chosen by User</param>
        /// <param name="type">Person Type</param>
        /// <returns>Array of Person</returns>
        public virtual Person[] LoadNames(
            SearchType searchType,
            string type)
        {
            IIMDBReader<Person> peopleReader =
                new IMDBFileReader<Person>(FilePeopleBasicsFull);

            if (PeopleLineCount == 0)
                PeopleLineCount = peopleReader.LineCount();

            int count = PeopleLineCount;
            IPrintable progressBar = new ProgressBar(new Rect(13, 10, 100, 1));
            progressBar.Print();

            IEnumerable<Person> people = null;

            try
            {
                switch (searchType)
                {
                    case SearchType.Name:
                        people = peopleReader.Read((int e) =>
                        {
                            ((ProgressBar)progressBar).Progress =
                                (float)e / (float)count;
                        })
                        .Where(x => x.PrimaryName.ToLower()
                        .Contains(type.ToLower()));
                        break;

                    case SearchType.BirthYear:
                        people = peopleReader.Read((int e) =>
                        {
                            ((ProgressBar)progressBar).Progress =
                                (float)e / (float)count;
                        })
                         .Where(x => x.BirthYear.ToString().ToLower()
                         .Contains(type.ToLower()));
                        break;

                    case SearchType.DeathYear:
                        people = peopleReader.Read((int e) =>
                        {
                            ((ProgressBar)progressBar).Progress =
                                (float)e / (float)count;
                        })
                       .Where(x => x.DeathYear.ToString().ToLower()
                       .Contains(type.ToLower()));
                        break;

                    case SearchType.Profession:
                        people = peopleReader.Read((int e) =>
                        {
                            ((ProgressBar)progressBar).Progress =
                                (float)e / (float)count;
                        })
                        .Where(x => x.PrimaryProfessions.ToStringArray()
                        .Contains(type.ToLower(), true));

                        break;
                    case SearchType.Custom:
                        string[] options = type.Split(',');
                        people = peopleReader.Read((int e) =>
                        {
                            ((ProgressBar)progressBar).Progress =
                                (float)e / (float)count;
                        })
                        .Where(x => x.PrimaryName.ToLower()
                        .Contains(options[0].ToLower()) &&
                                    x.BirthYear.ToString().ToLower()
                                    .Contains(options[1].ToLower()) &&
                                    x.DeathYear.ToString().ToLower()
                                    .Contains(options[2].ToLower()) &&
                                    x.PrimaryProfessions.ToStringArray()
                                    .Contains(options[3].ToLower(), true));
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }

            return people.ToArray();
        }

        /// <summary>
        /// Loads Titles with Person chosen by User
        /// </summary>
        /// <param name="person">Person Type</param>
        /// <returns>Dictionary of Titles</returns>
        public virtual IDictionary<string, Title> LoadTitles(
            Person person)
        {
            IIMDBReader<Title> titleReader =
                new IMDBFileReader<Title>(FileTitleBasicsFull);

            if (TitlesLineCount == 0)
                TitlesLineCount = titleReader.LineCount();

            int count = TitlesLineCount;
            int progress = 0;
            IPrintable progressBar = new ProgressBar(new Rect(12, 10, 104, 1));
            progressBar.Print();

            IDictionary<string, Title> values =
                titleReader.Read((int e) =>
                {
                    progress++;
                    ((ProgressBar)progressBar).Progress =
                        (float)progress / (float)count;
                })
                .Where(x => person.KnownForTitles.Contains(x.ID))?
                .GroupBy(x => x.ID).ToDictionary(t => t.Key, t => t.First());

            return values;
        }

        /// <summary>
        /// Selects Directors from given Crew
        /// </summary>
        /// <param name="people"> People's Database </param>
        /// <param name="crew">People's profession</param>
        /// <returns>Enumerable of Person</returns>
        public virtual IEnumerable<Person> LoadDirectors(
            IDictionary<string, Person> people,
            IDictionary<string, Crew> crew) => people
               .Where(x => crew.Select(y => 
               y.Value.DirectorsID).Any(v => v.Contains(x.Key)))
               .Select(x => x.Value);
        
        /// <summary>
        /// Selects Writers from given Crew
        /// </summary>
        /// <param name="people"> People's Database </param>
        /// <param name="crew">People's profession</param>
        /// <returns>Enumerable of Person</returns>
        public virtual IEnumerable<Person> LoadWriters(
            IDictionary<string, Person> people,
            IDictionary<string, Crew> crew) => people
                .Where(x => crew.Select(y => 
                y.Value.WritersID).Any(v => v.Contains(x.Key)))
                .Select(x => x.Value);

        /// <summary>
        /// Joins principals to People's dictionary
        /// </summary>
        /// <param name="people">People Dictionary</param>
        /// <param name="principals">Principal's Dictionary</param>
        /// <returns>Dictionary of Person and Principal</returns>
        public virtual IDictionary<Person, Principal> LoadCast(
            IDictionary<string, Person> people,
            IDictionary<string, Principal> principals) => 
            people.Join(principals.Select(x => x.Value),
                                 person => person.Key,
                                 principal => principal.PersonID,
                                 (p1, p2) => new { 
                                     Person = p1.Value, Principal = p2 })
                .GroupBy(t => t.Person.ID)
                .ToDictionary(t => t.First()?.Person, t => 
                t.First()?.Principal);
      
        /// <summary>
        /// Selects the People, Principals, Crew from the Title
        /// </summary>
        /// <param name="title">Title chosen by User</param>
        /// <returns>Enumerable of Dictionaries of IIMDBValue</returns>
        public virtual IEnumerable<IDictionary<string, IIMDBValue>> 
            LoadPeopleForTitle(
            KeyValuePair<Title, Rating> title)
        {
            ProgressBar progressBar = 
                new ProgressBar(new Rect(12, 10, 104, 1));

            IIMDBReader<Crew> crewReader =
                new IMDBFileReader<Crew>(FileCrewsBasicsFull);
            IIMDBReader<Principal> principalReader =
               new IMDBFileReader<Principal>(FilePrincipalsBasicsFull);
            IIMDBReader<Person> personReader =
               new IMDBFileReader<Person>(FilePeopleBasicsFull);

            if (CrewLineCount == 0)
                CrewLineCount = crewReader.LineCount();

            if (PrincipalsLineCount == 0)
                PrincipalsLineCount = principalReader.LineCount();

            if (PeopleLineCount == 0)
                PeopleLineCount = personReader.LineCount();

            int count = CrewLineCount +
                PrincipalsLineCount +
                PeopleLineCount;

            IDictionary<string, Principal> principals =
                new Dictionary<string, Principal>();

            int progress = 0;
            Thread t1 = new Thread(() =>
            {
                principals =
                       principalReader.Read((int e) =>
                       {
                           progress++;
                           progressBar.Progress = 
                           (float)progress / (float)count;
                       })
                       .Where(x => x.ID.Contains(title.Key.ID))
                       .GroupBy(t => t.PersonID)
                       .ToDictionary(t => t.Key, t => t.First());
            });

            IDictionary<string, Crew> crew =
                new Dictionary<string, Crew>();

            Thread t2 = new Thread(() =>
            {
                crew =
                    crewReader.Read((int e) =>
                    {
                        progress++;
                        progressBar.Progress = (float)progress / (float)count;
                    })
                    .Where(x => x.ID.Contains(title.Key.ID))
                    .GroupBy(x => x.ID)
                    .ToDictionary(t => t.Key, t => t.First());
            });

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            IDictionary<string, Person> people = personReader.Read((int e) =>
                {
                    progress++;
                    progressBar.Progress = (float)progress / (float)count;
                })
                .Where(x => principals.TryGetValue(x.ID, out Principal p) ||
                            crew.Select(y => 
                            y.Value.DirectorsID).Any(y => y.Contains(x.ID)) ||
                            crew.Select(y => 
                            y.Value.WritersID).Any(y => y.Contains(x.ID)))
                .ToDictionary(t => t.ID, t => t);

            return new IDictionary<string, IIMDBValue>[3]
                {
                    crew.ToDictionary(t => t.Key, t => 
                    (IIMDBValue)t.Value),
                    principals.ToDictionary(t => t.Key, t => 
                    (IIMDBValue)t.Value),
                    people.ToDictionary(t => t.Key, t => 
                    (IIMDBValue)t.Value)
                };
        }

        /// <summary>
        /// Loads Episode Information from Title
        /// </summary>
        /// <param name="title">Title selected by User</param>
        /// <returns>Value containing Title and Rating</returns>
        public virtual KeyValuePair<Title, Rating>? 
            LoadEpisodeTitle(Title title)
        {
            ProgressBar progressBar = 
                new ProgressBar(new Rect(12, 10, 104, 1));

            IIMDBReader<Episode> episodeReader =
                new IMDBFileReader<Episode>(FileEpisodesBasicsFull);

            if (EpisodeLineCount == 0)
                EpisodeLineCount = episodeReader.LineCount();

            int progress = 0;
            int count = EpisodeLineCount;
            progressBar.Print();
            Episode[] episodes = episodeReader.Read((int e) =>
                    {
                        progress++;
                        progressBar.Progress = (float)progress / (float)count;
                    })
                .Where(x => title.ID.Contains(x.ID)).ToArray();

            if (episodes.Length == 0)
                return null;

            return 
                LoadTitles(
                    SearchType.Episode, 
                    episodes.FirstOrDefault().ParentID).FirstOrDefault();
        }

        /// <summary>
        /// Counts File Line Count
        /// </summary>
        /// <param name="filename">File's full name</param>
        /// <returns>Returns Number of Lines</returns>
        protected virtual int LineCount(string filename)
        {
            int count = 0;
            Read(filename, (string e) => count++);
            return count;
        }

        /// <summary>
        /// Executes action for each line from File
        /// </summary>
        /// <param name="filename">File full name</param>
        /// <param name="action">Action for each line</param>
        protected virtual void Read(string filename, Action<string> action)
        {
            using (FileStream fs = new FileStream(
                filename, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        string line;
                        sr.ReadLine();
                        while ((line = sr.ReadLine()) != null)
                            action?.Invoke(line);
                    }
                }
            }
        }

        /// <summary>
        /// Loads Genres, Types Professions from Database
        /// </summary>
        protected virtual void LoadBasics()
        {
            ProgressBar progressBar = 
                new ProgressBar(new Rect(12, 10, 104, 1));

            int progress = 0;
            int count = 0;

            Thread a1 = new Thread(() =>
            {
                TitlesLineCount = LineCount(FileTitleBasicsFull);
            });

            Thread a2 = new Thread(() =>
            {
                PeopleLineCount = LineCount(FilePeopleBasicsFull);
            });

            a1.Start();
            a2.Start();
            a1.Join();
            a2.Join();

            count = TitlesLineCount + PeopleLineCount;

            Thread t1 = new Thread(() =>
            {
                Read(FileTitleBasicsFull, (string line) =>
                {
                    string[] fields = line.Split('\t');
                    Types.Add(fields[1]);
                    IEnumerable<string> titleGenres = fields[8].Split(',')
                             .Where(genre => genre != null &&
                                    genre.Length > 0 &&
                                    genre != @"\N");
                    foreach (string s in titleGenres)
                        Genres.Add(s);

                    progressBar.Progress = (float)++progress / (float)count;
                });
            });

            Thread t2 = new Thread(() =>
            {
                Read(FilePeopleBasicsFull, (string line) =>
                {
                    string[] fields = line.Split('\t')[4].Split(",");
                    foreach (string field in fields)
                        Professions.Add(field);

                    progressBar.Progress = (float)++progress / (float)count;
                });
            });

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
        }
    }
}