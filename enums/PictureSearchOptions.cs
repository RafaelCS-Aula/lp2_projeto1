using System;

namespace Projeto_imdb_analyser
{
    /// <summary>
    /// Enumerates the search options relevant to movies the user can pick.
    /// </summary>
    public enum PictureSearchOptions
    {
        // Title
        TITLE,
        // 18+ or not
        ADULT,
        // Launch year of movie/episode, or start year of show
        YEAR_LAUNCH,
        // ditto but end
        YEAR_END,        

    }
}