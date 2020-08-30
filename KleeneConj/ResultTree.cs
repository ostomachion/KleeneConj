using System;
using System.Collections.Generic;

namespace KleeneConj
{
    public abstract class ResultTree { }

    public interface IChildResultTree { }

    public class RootResultTree : ResultTree
    {
        public IEnumerable<IChildResultTree> Children { get; }

        public RootResultTree(IEnumerable<IChildResultTree> children)
        {
            Children = children;
        }
    }

    public class AcceptResultTree : ResultTree, IChildResultTree { }

    public class CharacterResultTree : ResultTree, IChildResultTree
    {
        public char Value { get; }

        public IEnumerable<IChildResultTree> Children { get; }

        public CharacterResultTree(char value, IEnumerable<IChildResultTree> children)
        {
            this.Value = value;
            this.Children = children;
        }
    }
}
