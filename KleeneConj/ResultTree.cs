using System;
using System.Collections.Generic;

namespace KleeneConj
{
    public abstract class ResultTree { }

    public class StructureResultTree : ResultTree
    {
        public string Name { get; }

        public IEnumerable<ResultTree> FirstChild { get; }

        public IEnumerable<ResultTree> NextSibling { get; }

        public StructureResultTree(string name, IEnumerable<ResultTree> firstChild, IEnumerable<ResultTree> nextSibling)
        {
            this.Name = name;
            this.NextSibling = nextSibling;
            this.FirstChild = firstChild;
        }
    }
}
