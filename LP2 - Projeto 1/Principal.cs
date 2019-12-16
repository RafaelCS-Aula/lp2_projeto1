using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace LP2___Projeto_1
{
    public class Principal : IMDBObject
    {
        public override string ID 
            => Line.ConvertToString().Split('\t')[0];
        public virtual string PersonID 
            => Line.ConvertToString().Split('\t')[1];
        public virtual string Category
            => Line.ConvertToString().Split('\t')[2];
        public virtual string Job 
            => Line.ConvertToString().Split('\t')[3];
        public virtual string Characters 
            => Line.ConvertToString().Split('\t')[4]
                   .Replace("[", "").Replace("]", "");

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

        public static Principal Parse(string line)
        {
            //0          1          2         3           4     5       
            //tconst     ordering   nconst    category    job   Characters

            return new Principal(line);
        }
    }
}
