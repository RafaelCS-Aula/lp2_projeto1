using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2___Projeto_1
{
    public class Person : IMDBObject
    {
        public override string ID
        {
            get => Line.ConvertToString().Split("\t")[0];
        }
        public virtual string PrimaryName
        {
            get => Line.ConvertToString().Split("\t")[1];
        }
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
        public virtual IEnumerable<IEnumerable<char>> PrimaryProfessions
        {
            get
            {
                return Line.ConvertToString().Split("\t")[4].Split(",")
                    .Select(x => x.ToCharArray());
            }
        }
        public virtual IEnumerable<IEnumerable<char>> KnownForTitles
        {
            get
            {
                string[] knownFor = Line.ConvertToString().Split("\t")[5].Split(",");
                return knownFor;
            }
        }

        public Person(string line)
        {
            Line = line.ToCharArray();
        }

        public static Person Parse(string line)
        {
            //0          1             2             3              4                   5          
            //tconst     primaryName   birthYear     deathYear      primaryProfession   knownForTitles
            return new Person(line);
        }

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

        public override string ToString()
        {
            return PrimaryName.ConvertToString() + "\t" +
                BirthYear + " / " + DeathYear + "\t" +
                PrimaryProfessions.ToStringArray().ToStringArray(3);
        }
    }
}