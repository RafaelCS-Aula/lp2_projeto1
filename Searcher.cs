using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_imdb_analyser
{
    /// <summary>
    /// Class to cross reference the sorting options and the databse of files
    /// </summary>
    public class Searcher
    {
        ICollection<IMenuSelectable> sortingRequisites;

        IEnumerable<dbItem> dataBaseToBeSearched;
        IEnumerable<dbItem> currentItems;

        List<dbItem> ass;


        void checkRequisites()
        {
            // Check for title
            foreach (IMenuSelectable r in sortingRequisites)
            {
                if(r.GetType() == typeof(TitleValueSortOption)) 
                dataBaseToBeSearched  =  CullByTitle(r.finalResult as string); 

                else if(r.GetType() == typeof(AdultValueSortOption))
                dataBaseToBeSearched  =  CullByAdult((bool)r.finalResult);

                else if(r.GetType() == typeof(GenreValueSortOption))
                dataBaseToBeSearched  =  
                        CullByGenre(r.finalResult as GenreOptions[]);

                else if(r.GetType() == typeof(TypeValueSortOption))
                dataBaseToBeSearched  =  
                        CullByType((PictureTypeOptions)r.finalResult);
            }


        }

        /// <summary>
        /// Removes all of the items that dont match the criteria
        /// </summary>
        /// <param name="title"> Title tof what you want to stay </param>
        /// <returns></returns>
        IEnumerable<dbItem> CullByTitle(string title)
        {
            
            currentItems = from t in dataBaseToBeSearched
                where t.ItemTitle == title
                select t;
            return currentItems;
        }
        IEnumerable<dbItem> CullByAdult(bool isAdult)
        {
            
            currentItems = from t in dataBaseToBeSearched
                where t.IsAdult == isAdult
                select t;
            return currentItems;
        }

        IEnumerable<dbItem> CullByGenre(GenreOptions[] gList)
        {
            currentItems = from t in dataBaseToBeSearched
                where t.ItemGenres.Equals(gList)
                select t;
            return currentItems;

        }
        IEnumerable<dbItem> CullByType(PictureTypeOptions p)
        {
            currentItems = from t in dataBaseToBeSearched
                where t.PictureType == p
                select t;
            return currentItems;
        }
        
        IEnumerable<dbItem> CullByReleaseYear(int releaseYear)
        {
            currentItems = from t in dataBaseToBeSearched
                where t.YearGap[0] == releaseYear
                select t;
            return currentItems;

        }
        IEnumerable<dbItem> CullByEndYear(int endYear)
        {
            currentItems = from t in dataBaseToBeSearched
                where t.YearGap[1] == endYear
                select t;
            return currentItems;

        }

        IEnumerable<dbItem> CullByReleaseGap(int[] lifeSpan)
        {
            int diff = lifeSpan[1] - lifeSpan[0]; 

            currentItems = from t in dataBaseToBeSearched
                where t.YearGap[0] + diff <= lifeSpan[1]
                select t;
            return currentItems;

        }

        IEnumerable<dbItem> CullByRating(int rate)
        {

            currentItems = from t in dataBaseToBeSearched
                where t.Rating == rate
                select t;
            return currentItems;


        }





    }
}