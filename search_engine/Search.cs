using System;
using System.Collections;
using System.Collections.Generic;

namespace Projeto_imdb_analyser
{
    /// <summary>
    /// Class receives the search options from the input classes and organises
    /// it.!-- This class is responsible for turning that info and making it
    /// ready to be used to search the files  
    /// 
    /// !-- WIP class, probably overcomplicating
    /// </summary>
    public class Search
    {
        private BroadSearchOptions _broadSearch;
        private bool _adult;
        private HashSet<GenreOptions> _genres;
        private PictureTypeOptions _pictureType;

        // start year and end year of show/movie
        private int?[,] _timeframe;

        /// <summary>
        /// Create a search and feed it the info from inputs
        /// </summary>
        /// <param name="b"> Picture or Person</param>
        /// <param name="picType"> Type of picture if applicable </param>
        /// <param name="searchOptions"> All search options</param>
        public Search(BroadSearchOptions b, ICollection<PictureSearchOptions> searchOptions,  PictureTypeOptions? picType = null, ICollection<GenreOptions> genres = null)
        {
            // Check if there is any option about adult setting
            _adult = searchOptions.Contains(PictureSearchOptions.ADULT);

            // this way of implementing genres might be redundant
            // maybe for organisation's sake
            // make input handling and option collection, see how that works

        }

    }
}