using System;
using System.Collections.Generic;
using System.Text;

namespace LP2___Projeto_1
{
    public abstract class IMDBObject : IIMDBValue
    {
        public abstract string ID { get; }

        protected IEnumerable<char> Line { get; set; }
    }
}
