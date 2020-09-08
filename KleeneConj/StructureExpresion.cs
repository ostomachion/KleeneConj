using System.Collections.Generic;
using System.Linq;

namespace KleeneConj
{
    public class StructureExpression : Expression
    {
        public string Name { get; }
        public Expression Value { get; }

        public StructureExpression(string name)
        {
            this.Name = name;
            this.Value = GroupExpression.Empty;
        }

        public StructureExpression(string name, Expression value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override IEnumerable<ResultTree> Run()
        {
            yield return new StructureResultTree(this.Name, this.Value.Run(), EnumerableExt.Yield<ResultTree>(null));
        }
    }
}