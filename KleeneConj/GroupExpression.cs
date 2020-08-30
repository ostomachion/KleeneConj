using System;
using System.Collections.Generic;
using System.Linq;

namespace KleeneConj
{
    public class GroupExpression : Expression
    {
        public IEnumerable<Expression> Value { get; }

        public GroupExpression(IEnumerable<Expression> value)
        {
            Value = value;
        }

        public override RootResultTree Run()
        {
            if (this.Value.Any())
            {
                var head = this.Value.First().Run();
                var tail = new GroupExpression(this.Value.Skip(1)).Run();

                return new RootResultTree(head.Children.SelectMany(child => concat(child, tail)));
            }
            else
            {
                return new RootResultTree(EnumerableExt.Yield(new AcceptResultTree()));
            }

            static IEnumerable<IChildResultTree> concat(IChildResultTree head, RootResultTree tail)
            {
                if (head is AcceptResultTree)
                {
                    return tail.Children;
                }
                else if (head is CharacterResultTree c)
                {
                    return EnumerableExt.Yield(new CharacterResultTree(c.Value,
                        c.Children.SelectMany(x => concat(x, tail))));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}