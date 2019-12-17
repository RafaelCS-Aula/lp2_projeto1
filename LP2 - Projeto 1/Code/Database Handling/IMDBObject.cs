using System.Collections.Generic;

namespace LP2___Projeto_1
{
    public abstract class IMDBObject : IIMDBValue
    {
        public abstract string ID { get; }

        protected IEnumerable<char> Line { get; set; }
    }
}
