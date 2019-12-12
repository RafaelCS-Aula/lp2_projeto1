namespace Projeto_imdb_analyser
{
    /// <summary>
    /// Class to hold info about one item of the database
    /// </summary>
    public class dbItem
    {
        private string _databaseId {get; set;}
        public string ItemTitle {get; set;}
        public bool IsAdult {get; set;}

        public GenreOptions[] ItemGenres {get; set;} = new GenreOptions[3];

        public PictureTypeOptions PictureType {get; set;}

        // Launch date is [0] end date is [1]
        // in the case of movies they are the same
        public int[] YearGap {get; set;} = new int[2];

        public int Rating {get; set;}
    }
}