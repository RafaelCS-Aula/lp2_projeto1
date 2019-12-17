using System;

namespace LP2___Projeto_1
{
    /// <summary>
    /// The Class for Rating
    /// </summary>
    public class Rating : IMDBObject
    {
        /// <summary>
        /// Rating ID
        /// </summary>
        public override string ID => 
            Line.ConvertToString().Split("\t")[0];
        
        /// <summary>
        /// Returns Average Rating (if available)
        /// </summary>
        public virtual float? AvarageRating
        {
            get
            {
                float? avarage = float.TryParse(
                    Line.ConvertToString().Split("\t")[1]
                    .Replace(".", ","), out float aux) ?
                   (float?)aux :
                   null;
                return avarage;
            }
        }

        /// <summary>
        /// Returns Number of Votes (if available)
        /// </summary>
        public virtual uint? NumberOfVotes
        {
            get
            {
                uint? numberOfVotes = uint.TryParse(
                    Line.ConvertToString().Split("\t")[2], 
                    out uint aux2) ?
                 (uint?)aux2 :
                 null;
                return numberOfVotes;
            }
        }

        /// <summary>
        /// Constructor, Recieves Line from Database
        /// </summary>
        /// <param name="line">Line from Database</param>
        public Rating(string line)
        {
            Line = line.ToCharArray();
        }

        /// <summary>
        /// Print's available Rating and Number of Votes information
        /// </summary>
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Rating : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(AvarageRating == null ?
                "N/A\t" : 
                AvarageRating.Value.ToString("f1") + "\t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Number Of Votes : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(NumberOfVotes == null ? 
                "N/A" : 
                NumberOfVotes.Value.ToString());
        }

        /// <summary>
        /// Parses Rating from Database Line
        /// </summary>
        /// <param name="line">Database Line</param>
        /// <returns>Rating</returns>
        public static Rating Parse(string line)
        {
            //0          1              2            
            //tconst     avarageRating  numberOfVotes

            return new Rating(line);
        }
    }
}