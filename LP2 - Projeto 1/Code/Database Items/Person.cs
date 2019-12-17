using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    /// <summary>
    /// The class for Person Information
    /// </summary>
    public class Person : IMDBObject
    {
        /// <summary>
        /// Person ID
        /// </summary>
        public override string ID 
            => Line.ConvertToString().Split("\t")[0];
        
        /// <summary>
        /// Person's PrimaryName
        /// </summary>
        public virtual string PrimaryName
            => Line.ConvertToString().Split("\t")[1];
        
        /// <summary>
        /// Returns Person Birth Year
        /// </summary>
        public virtual short? BirthYear
        {
            get
            {
                short? birthYear = short.TryParse(Line.ConvertToString()
                                        .Split("\t")[2], out short aux)
                    ? (short?)aux
                    : null;
                return birthYear;
            }
        }

        /// <summary>
        /// Returns Person Death Year
        /// </summary>
        public virtual short? DeathYear
        {
            get
            {
                short? deathYear = short.TryParse(Line.ConvertToString()
                                        .Split("\t")[3], out short aux)
                    ? (short?)aux
                    : null;
                return deathYear;
            }
        }

        /// <summary>
        /// Returns Person Primary Professions
        /// </summary>
        public virtual IEnumerable<IEnumerable<char>> PrimaryProfessions
            => Line.ConvertToString().Split("\t")[4].Split(",")
                    .Select(x => x.ToCharArray());

        /// <summary>
        /// Returns Titles for which the Person's known for  
        /// </summary>
        public virtual IEnumerable<IEnumerable<char>> KnownForTitles
            => Line.ConvertToString().Split("\t")[5].Split(",");
        
        /// <summary>
        /// Constructor, Recieves line from Database
        /// </summary>
        /// <param name="line">Line from Database</param>
        public Person(string line)
        {
            Line = line.ToCharArray();
        }

        /// <summary>
        /// Parses Person from Database Line
        /// </summary>
        /// <param name="line">Line from Database</param>
        /// <returns>Person</returns>
        public static Person Parse(string line)
        {
            //0          1             2             3              4                   5          
            //tconst     primaryName   birthYear     deathYear      primaryProfession   knownForTitles
            return new Person(line);
        }

        /// <summary>
        /// Renders Person Information
        /// </summary>
        public void FormatOutput()
        {
            "Name : ".Print(
                ConsoleColor.Yellow,
                ConsoleColor.Black,
                false);
            PrimaryName.Print();

            "Birth Year : ".Print(
                ConsoleColor.Yellow,
                ConsoleColor.Black, 
                false);
            string b = BirthYear.HasValue ?
                BirthYear.ToString() :
                "N/A";
            b.Print();

            "Death Year : ".Print(
                ConsoleColor.Yellow, 
                ConsoleColor.Black, 
                false);
            string d = DeathYear.HasValue ?
                DeathYear.ToString() :
                "N/A";
            d.Print();
           
            "Primary Professions : ".Print(
                ConsoleColor.Yellow,
                ConsoleColor.Black, 
                false);
            PrimaryProfessions
                .ToStringArray()
                .Print(ConsoleColor.White, 
                       ConsoleColor.Black, 
                       false);
        }

        /// <summary>
        /// Returns Person Information in a String 
        /// </summary>
        /// <returns>Person in String</returns>
        public override string ToString()
        {
            return PrimaryName.ConvertToString() + "\t" +
                BirthYear + " / " + DeathYear + "\t" +
                PrimaryProfessions.ToStringArray().ToStringArray(3);
        }
    }
}