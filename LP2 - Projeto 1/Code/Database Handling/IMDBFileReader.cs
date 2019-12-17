using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Has methods for reading files
    /// </summary>
    /// <typeparam name="T">IIMDB value</typeparam>
    public class IMDBFileReader<T> : IIMDBReader<T> where T : IIMDBValue
    {
        private string Filename { get; }

        public IMDBFileReader(string filename)
        {
            Filename = filename;
        }

        /// <summary>
        /// In the method Parse, the line gets searched for its type 
        /// information
        /// </summary>
        /// <param name="line">Database line being interpreted</param>
        /// <returns>Returns IIMDBValue</returns>        
        public virtual T Parse(string line)
        {
            IIMDBValue obj = null;

            if (typeof(Title) == typeof(T))
                obj = Title.Parse(line);
            else if (typeof(Person) == typeof(T))
                obj = Person.Parse(line);
            else if (typeof(Crew) == typeof(T))
                obj = Crew.Parse(line);
            else if (typeof(Rating) == typeof(T))
                obj = Rating.Parse(line);
            else if (typeof(Principal) == typeof(T))
                obj = Principal.Parse(line);
            else if (typeof(Episode) == typeof(T))
                obj = Episode.Parse(line);

            return (T)obj;
        }

        /// <summary>
        /// Returns number of lines in selected file
        /// </summary>
        /// <returns>Number of Lines in Selected File</returns>
        public virtual int LineCount()
        {
            int count = 0;
            using (FileStream fs = new FileStream(
              Filename, FileMode.Open, FileAccess.Read))
            {
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        string line;
                        sr.ReadLine();
                        while ((line = sr.ReadLine()) != null)
                            count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Decompresses and Reads File
        /// </summary>
        /// <param name="onRead"> Method to call for each line of the 
        /// table </param>
        /// <returns>Enumerable of 'IIMDBValue'</returns>
        public virtual IEnumerable<T> Read(Action<int> onRead)
        {
            using (FileStream fs = new FileStream(
                Filename, FileMode.Open, FileAccess.Read))
            {
                int count = 0;
                using (GZipStream gzs = new GZipStream(
                    fs, CompressionMode.Decompress))
                {
                    using (StreamReader sr = new StreamReader(gzs))
                    {
                        string line;
                        sr.ReadLine();
                        while ((line = sr.ReadLine()) != null)
                        {
                            onRead?.Invoke(++count);
                            yield return Parse(line);
                        }
                    }
                }
            }
        }
    }
}