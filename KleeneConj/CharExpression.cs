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

        public override IEnumerable<ResultTree> Run()
        {
            yield return new CharacterResultTree(this.Value);
        }
    }
}