using System;

namespace LP2___Projeto_1
{
    public class Rating : IMDBObject
    {
        public override string ID => 
            Line.ConvertToString().Split("\t")[0];
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

        public Rating(string line)
        {
            Line = line.ToCharArray();
        }

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

        public static Rating Parse(string line)
        {
            //0          1              2            
            //tconst     avarageRating  numberOfVotes

            return new Rating(line);
        }
    }
}