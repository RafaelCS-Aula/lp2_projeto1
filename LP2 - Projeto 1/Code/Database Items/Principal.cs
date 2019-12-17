namespace LP2___Projeto_1
{
    /// <summary>
    /// The Class for Principal
    /// </summary>
    public class Principal : IMDBObject
    {
        /// <summary>
        /// Principal ID
        /// </summary>
        public override string ID 
            => Line.ConvertToString().Split('\t')[0];

        /// <summary>
        /// Person ID
        /// </summary>
        public virtual string PersonID 
            => Line.ConvertToString().Split('\t')[1];

        /// <summary>
        /// Principal Category
        /// </summary>
        public virtual string Category
            => Line.ConvertToString().Split('\t')[2];

        /// <summary>
        /// Principal Job
        /// </summary>
        public virtual string Job 
            => Line.ConvertToString().Split('\t')[3];

        /// <summary>
        /// Principal's Characters
        /// </summary>
        public virtual string Characters 
            => Line.ConvertToString().Split('\t')[4]
                   .Replace("[", "").Replace("]", "");

        /// <summary>
        /// Constructor, Receives Line from Database
        /// </summary>
        /// <param name="line">Line from Database</param>
        public Principal(string line)
        {
            string[] s = line.Split("\t");
            Line = string.Join("\t",
                new string[]
                {
                    s[0], //0 - ID
                    s[2], //1 - PersonID
                    s[3], //2 - Category
                    s[4], //3 - Job
                    s[5], //4 - Characters
                }).ToCharArray();
        }

        /// <summary>
        /// Parses Principal from Database Line
        /// </summary>
        /// <param name="line">Database Line</param>
        /// <returns>Principal</returns>
        public static Principal Parse(string line)
        {
            //0          1          2         3           4     5       
            //tconst     ordering   nconst    category    job   Characters

            return new Principal(line);
        }
    }
}
