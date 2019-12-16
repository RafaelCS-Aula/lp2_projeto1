using System.Collections.Generic;
using System;
using System.Linq;

namespace LP2___Projeto_1
{
    public class Title : IMDBObject
    {
        public override string ID 
            => Line.ConvertToString().Split("\t")[0];
        public virtual string PrimaryTitle 
            => Line.ConvertToString().Split("\t")[2];
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
        public virtual short? EndYear
        {
            get
            {
                short? endYear = short.TryParse(Line.ConvertToString().Split("\t")[5], out short aux2)
                    ? (short?)aux2
                    : null;
                return endYear;
            }
        }
        public virtual IEnumerable<IEnumerable<char>> Genres
        {
            get
            {
                return Line.ConvertToString().Split("\t")[6].Split(',')
                    .Where(genre => genre != null && genre.Length > 0 && genre != @"\N")
                    .Select(x => x.ToCharArray());
            }
        }
        public virtual string Type
        {
            get
            {
                return Line.ConvertToString().Split("\t")[1];
            }
        }
        public virtual bool IsAdult
        {
            get
            {
                return Line.ConvertToString().Split("\t")[3].Equals("0") ? false : true;
            }
        }
        public virtual bool IsEpisode
            => Type.Contains("tvEpisode");
        public virtual bool IsSeries
            => Type.Contains("tvSeries");

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

        public static Title Parse(string line)
        {
            //0          1          2             3              4        5          6       7               8
            //tconst     titleType  primaryTitle  originalTitle  isAdult  startYear  endYear runtimeMinutes  genres
            //tt0000001  short      Carmencita    Carmencita     0        1894       \N      1               Documentary,Short

            return new Title(line);
        }

        public override string ToString()
        {
            return PrimaryTitle.ConvertToString() + "\t" +
                (StartYear != null ? StartYear.ToString() : "") + "\t" +
                Genres.ToStringArray(2);
        }
    }
}