using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Class for Title
    /// </summary>
    public class Title : IMDBObject
    {
        /// <summary>
        /// Title ID
        /// </summary>
        public override string ID 
            => Line.ConvertToString().Split("\t")[0];

        /// <summary>
        /// Title's Primary Name
        /// </summary>
        public virtual string PrimaryTitle 
            => Line.ConvertToString().Split("\t")[2];

        /// <summary>
        /// Returns Title's StartYear (if available)
        /// </summary>
        public virtual short? StartYear
        {
            get
            {
                short? startYear = short.TryParse(
                        Line.ConvertToString().Split("\t")[4],
                        out short aux)
                    ? (short?)aux
                    : null;
                return startYear;
            }
        }

        /// <summary>
        /// Returns Title's EndYear (if available)
        /// </summary>
        public virtual short? EndYear
        {
            get
            {
                short? endYear = short.TryParse(Line.ConvertToString()
                    .Split("\t")[5], out short aux2)
                    ? (short?)aux2
                    : null;
                return endYear;
            }
        }

        /// <summary>
        /// Returns Title Genres
        /// </summary>
        public virtual IEnumerable<IEnumerable<char>> Genres 
            => Line.ConvertToString().Split("\t")[6].Split(',')
                    .Where(genre => genre != null &&
                    genre.Length > 0 &&
                    genre != @"\N")
                    .Select(x => x.ToCharArray());

        /// <summary>
        /// Returns Title Type
        /// </summary>
        public virtual string Type => Line.ConvertToString().Split("\t")[1];

        /// <summary>
        /// Returns bool on "if Adult"
        /// </summary>
        public virtual bool IsAdult 
            => Line.ConvertToString()
            .Split("\t")[3].Equals("0") ? false : true;

        /// <summary>
        /// Returns bool on "if Episode"
        /// </summary>
        public virtual bool IsEpisode
            => Type.Contains("tvEpisode");

        /// <summary>
        /// Returns bool on "if Series"
        /// </summary>
        public virtual bool IsSeries
            => Type.Contains("tvSeries");

        /// <summary>
        /// Constructor, Recieves Line from Database
        /// </summary>
        /// <param name="line">Line from Database</param>
        public Title(string line)
        {
            string[] s = line.Split("\t");
            Line = string.Join("\t", 
                new string[] 
                {
                    s[0], //0 - ID
                    s[1], //1 - Type
                    s[2], //2 - PrimaryTitle
                    s[4], //3 - IsAdult
                    s[5], //4 - StartYear
                    s[6], //5 - EndYear
                    s[8]  //6 - Genres
                }).ToCharArray();
        }

        /// <summary>
        /// Parse Title from Database Line
        /// </summary>
        /// <param name="line">Database Line</param>
        /// <returns>Title</returns>
        public static Title Parse(string line)
        {
            //0          1          2             3              4        5          6       7               8
            //tconst     titleType  primaryTitle  originalTitle  isAdult  startYear  endYear runtimeMinutes  genres
            //tt0000001  short      Carmencita    Carmencita     0        1894       \N      1               Documentary,Short

            return new Title(line);
        }

        /// <summary>
        /// Converts Title Information to String
        /// </summary>
        /// <returns>Title Information in String</returns>
        public override string ToString()
        {
            return PrimaryTitle.ConvertToString() + "\t" +
                (StartYear != null ? StartYear.ToString() : "") + "\t" +
                Genres.ToStringArray(2);
        }
    }
}