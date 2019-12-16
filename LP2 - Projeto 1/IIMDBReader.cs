using System;
using System.Collections.Generic;
using System.Text;

namespace LP2___Projeto_1
{
    public interface IIMDBReader<T> where T : IIMDBValue
    {
        IEnumerable<T> Read(Action<int> onRead);

        int LineCount();

        T Parse(string line);
    }
}
