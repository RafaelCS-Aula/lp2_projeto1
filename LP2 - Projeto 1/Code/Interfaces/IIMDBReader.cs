using System;
using System.Collections.Generic;

namespace LP2___Projeto_1
{
    /// <summary>
    /// Reader Interface
    /// </summary>
    /// <typeparam name="T">Type that implements IIMDValue</typeparam>
    public interface IIMDBReader<T> where T : IIMDBValue
    {
        IEnumerable<T> Read(Action<int> onRead);

        int LineCount();

        T Parse(string line);
    }
}
