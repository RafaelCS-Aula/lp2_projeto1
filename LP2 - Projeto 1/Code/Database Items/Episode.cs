
namespace LP2___Projeto_1
{
    /// <summary>
    /// The class for Episode Information
    /// </summary>
    public class Episode : IMDBObject
    {
        /// <summary>
        /// Episode ID
        /// </summary>
        public override string ID
            => Line.ConvertToString().Split("\t")[0];

        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual string ParentID
            => Line.ConvertToString().Split("\t")[1];

        /// <summary>
        /// Episode Season
        /// </summary>
        public virtual string Season
            => Line.ConvertToString().Split("\t")[2];

        /// <summary>
        /// Episode Number
        /// </summary>
        public virtual string Number
            => Line.ConvertToString().Split("\t")[3];

        /// <summary>
        /// Constructor, Recieves Line from Database
        /// identified as Episode
        /// </summary>
        /// <param name="line">Line from Database</param>
        public Episode(string line)
        {
            Line = line.ToCharArray();
        }

        /// <summary>
        /// Creates Line identifying episode
        /// </summary>
        /// <returns>Episode in String</returns>
        public override string ToString()
        {
            return "Episode nº " + Number + "\tSeason : " + Season;
        }

        /// <summary>
        /// Parses Line from Database
        /// identified as Episode
        /// </summary>
        /// <param name="line">Line from Database</param>
        /// <returns>Episode</returns>
        public static Episode Parse(string line)
        {
            return new Episode(line);
        }
    }
}