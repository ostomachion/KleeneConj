using System.Collections.Generic;
using System.Linq;

namespace KleeneConj
{
    public class AltExpression : Expression
    {
        public IEnumerable<Expression> Value { get; }

        public AltExpression(IEnumerable<Expression> value)
        {
            Value = value;
        }

        public override RootResultTree Run()
        {
            return new RootResultTree(this.Value.SelectMany(x => x.Run().Children));
        }
    }
}
