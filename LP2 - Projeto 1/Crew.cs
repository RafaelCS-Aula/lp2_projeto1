using System.Collections.Generic;

namespace LP2___Projeto_1
{
    public class Crew : IMDBObject
    {
        public override string ID
            => Line.ConvertToString().Split("\t")[0];
        public virtual IEnumerable<string> DirectorsID 
            => Line.ConvertToString().Split("\t")[1].Split(',');
        public virtual IEnumerable<string> WritersID
            => Line.ConvertToString().Split("\t")[2].Split(',');
        public Crew(string line)
        {
            Line = line.ToCharArray();
        }

        public static Crew Parse(string line)
        {
            //0          1          2      
            //tconst     directors  writers

            return new Crew(line);
        }
    }
}
