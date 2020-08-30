using System.Collections.Generic;

namespace KleeneConj
{
    public static class EnumerableExt
    {
        public static IEnumerable<T> Yield<T>(T item) { yield return item; }
    }
}