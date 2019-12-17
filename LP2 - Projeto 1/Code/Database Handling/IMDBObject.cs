using System.Collections.Generic;

namespace LP2___Projeto_1
{
    /// <summary>
    /// The base class for each Database Info
    /// </summary>
    public abstract class IMDBObject : IIMDBValue
    {
        /// <summary>
        /// IMDBObject ID Value
        /// </summary>
        public abstract string ID { get; }

        protected IEnumerable<char> Line { get; set; }
    }
}
