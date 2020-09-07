using System;
using System.Collections.Generic;

namespace KleeneConj
{
    public abstract class ResultTree { }

    public class CharacterResultTree : ResultTree
    {
        public char Value { get; }

        public IEnumerable<ResultTree> Next { get; }

        public CharacterResultTree(char value)
        {
            this.Value = value;
            this.Next = EnumerableExt.Yield<ResultTree>(null);
        }

        public CharacterResultTree(char value, IEnumerable<ResultTree> next)
        {
            this.Value = value;
            this.Next = next;
        }
    }
}
