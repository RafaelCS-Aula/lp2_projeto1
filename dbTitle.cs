namespace Projeto_imdb_analyser
{

    public class dbTitle: dbItem
    {
        public short DatabaseId {get; private set;}
        public string ItemTitle {get; private set;}
        public bool IsAdult {get; private  set;}

        public GenreOptions[] ItemGenres {get; private set;} = new GenreOptions[3];

        public PictureTypeOptions PictureType {get; private set;}

        // Launch date is [0] end date is [1]
        // in the case of movies they are the same
        public int?[] YearGap {get; private set;} = new int?[2];

        public float Rating {get; private set;}

        // Show Specific Properties
        public short? ParentID {get; private set;}
        public short? Season {get; private set;}
        public short? EpisodeN {get; private set;}

        /// <summary>
        ///  class for items from the title.basics database
        /// </summary>
        /// <param name="tconst">alphanumeric unique identifier of the title/episode</param>
        /// <param name="titleType"> the type/format of the title (e.g. movie,
        ///  short, tvseries, tvepisode, video, etc)</param>
        /// <param name="title"> the more popular title / the title used by the
        ///  filmmakers on promotional materials at the point of release </param>
        /// <param name="isAdult"> IS rated for Adults</param>
        /// <param name="genres"> includes up to three genres associated with
        ///  the title</param>
        /// <param name="startYear">represents the release year of a title. In the case of TV Series, it is the series start year </param>
        /// <param name="endYear">TV Series end year.</param>
        public dbTitle(short tconst, PictureTypeOptions titleType, string title, bool isAdult, GenreOptions[] genres, float rating, int startYear, int? endYear = 0, short? parentTconst = 0, short? season = 0, short? episodeNumber = 0)
        {
            DatabaseId = tconst;
            ItemTitle = title;
            PictureType = titleType;        
            this.IsAdult = isAdult;
            ItemGenres = genres;
            YearGap[0] = startYear;
            YearGap[1] = endYear;
            Rating = rating;

            // Show Specific
            ParentID = parentTconst;
            Season = season;
            EpisodeN = episodeNumber;




        }
    }
}