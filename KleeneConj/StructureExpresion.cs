using System.Collections.Generic;
using System.Linq;

namespace KleeneConj
{
    public class StructureExpression : Expression
    {
        public string Name { get; }

        public StructureExpression(string name)
        {
            Name = name;
        }

        public override IEnumerable<ResultTree> Run()
        {
            yield return new StructureResultTree(this.Name);
        }
    }
}