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

        public override RootResultTree Run()
        {
            var first = this.First.Run();
            var second = this.Second.Run();

            return new RootResultTree(Test(first.Children, second.Children));

            static IEnumerable<IChildResultTree> Test(IEnumerable<IChildResultTree> leader, IEnumerable<IChildResultTree> follower)
            {
                foreach (var l in leader)
                {
                    if (l is AcceptResultTree)
                    {
                        foreach (var f in follower.OfType<AcceptResultTree>())
                        {
                            yield return f;
                        }
                    }
                    else if (l is CharacterResultTree c)
                    {
                        foreach (var f in follower.OfType<CharacterResultTree>().Where(x => x.Value == c.Value))
                        {
                            yield return new CharacterResultTree(f.Value, Test(c.Children, f.Children));
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
