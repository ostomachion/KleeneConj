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

        public override IEnumerable<ResultTree> Run()
        {
            if (this.Value.Any())
            {
                var head = this.Value.First().Run();
                var tail = new GroupExpression(this.Value.Skip(1)).Run();

                return head.SelectMany(child => concat(child, tail));
            }
            else
            {
                return EnumerableExt.Yield<ResultTree>(null);
            }

            static IEnumerable<ResultTree> concat(ResultTree head, IEnumerable<ResultTree> tail)
            {
                if (head is null)
                {
                    return tail;
                }
                else if (head is StructureResultTree c)
                {
                    return EnumerableExt.Yield(new StructureResultTree(c.Name,
                        c.Next.SelectMany(x => concat(x, tail))));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}