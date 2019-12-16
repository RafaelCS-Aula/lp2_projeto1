using System;
using System.Collections.Generic;
using System.Text;

namespace LP2___Projeto_1
{
    public class Episode : IMDBObject
    {
        public override string ID
            => Line.ConvertToString().Split("\t")[0];
        public virtual string Parent
            => Line.ConvertToString().Split("\t")[1];
        public virtual string Season
            => Line.ConvertToString().Split("\t")[2];
        public virtual string Number
            => Line.ConvertToString().Split("\t")[3];

        public Episode(string line)
        {
            Line = line.ToCharArray();
        }

        public static Episode Parse(string line)
        {
            return new Episode(line);
        }
    }
}