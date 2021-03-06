using System;
using System.Collections.Generic;
using System.Linq;

namespace KleeneConj
{
    public class ConjExpression : Expression
    {
        public Expression First { get; }
        public Expression Second { get; }

        public ConjExpression(Expression first, Expression second)
        {
            this.First = first;
            this.Second = second;
        }

        public override IEnumerable<ResultTree> Run()
        {
            var first = this.First.Run();
            var second = this.Second.Run();

            return Overlap(first, second);

            static IEnumerable<ResultTree> Overlap(IEnumerable<ResultTree> leader, IEnumerable<ResultTree> follower)
            {
                foreach (var l in leader)
                {
                    if (l is null)
                    {
                        foreach (var f in follower.Where(x => x is null))
                        {
                            yield return f;
                        }
                    }
                    else if (l is StructureResultTree c)
                    {
                        foreach (var f in follower.OfType<StructureResultTree>().Where(x => x.Name == c.Name))
                        {
                            yield return new StructureResultTree(f.Name, Overlap(c.FirstChild, f.FirstChild), Overlap(c.NextSibling, f.NextSibling));
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
