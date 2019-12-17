using System.Collections.Generic;

namespace LP2___Projeto_1
{
    /// <summary>
    /// The class for the Crew Information
    /// </summary>
    public class Crew : IMDBObject
    {
        /// <summary>
        /// Crew ID
        /// </summary>
        public override string ID
            => Line.ConvertToString().Split("\t")[0];

        /// <summary>
        /// Directors ID
        /// </summary>
        public virtual IEnumerable<string> DirectorsID 
            => Line.ConvertToString().Split("\t")[1].Split(',');

        /// <summary>
        /// Writers ID
        /// </summary>
        public virtual IEnumerable<string> WritersID
            => Line.ConvertToString().Split("\t")[2].Split(',');

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="line">Line from Database</param>
        public Crew(string line)
        {
            Line = line.ToCharArray();
        }

        /// <summary>
        /// Parses Crew from Database Line
        /// </summary>
        /// <param name="line">Line from Database</param>
        /// <returns>Crew</returns>
        public static Crew Parse(string line)
        {
            //0          1          2      
            //tconst     directors  writers

            return new Crew(line);
        }
    }
}
