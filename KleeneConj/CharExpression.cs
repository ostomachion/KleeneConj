using System.Collections.Generic;
using System.Linq;

namespace KleeneConj
{
    public class CharExpression : Expression
    {
        public char Value { get; }

        public CharExpression(char value)
        {
            Value = value;
        }

        public override RootResultTree Run()
        {
            return new RootResultTree(EnumerableExt.Yield(new CharacterResultTree(this.Value, EnumerableExt.Yield(new AcceptResultTree()))));
        }
    }
}