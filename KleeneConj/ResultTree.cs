using System;
using System.Collections.Generic;

namespace KleeneConj
{
    public abstract class ResultTree { }

    public class StructureResultTree : ResultTree
    {
        public string Name { get; }

        public IEnumerable<ResultTree> Next { get; }

        public StructureResultTree(string name)
        {
            this.Name = name;
            this.Next = EnumerableExt.Yield<ResultTree>(null);
        }

        public StructureResultTree(string name, IEnumerable<ResultTree> next)
        {
            this.Name = name;
            this.Next = next;
        }
    }
}
