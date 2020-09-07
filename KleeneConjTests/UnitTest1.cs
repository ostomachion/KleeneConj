using System;
using KleeneConj;
using Xunit;

namespace KleeneConjTests
{
    public class UnitTest1
    {
        [Fact]
        public void CharTest()
        {
            var expr = new CharExpression('x');
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item => Assert.Null(item)
                    );
                }
            );
        }

        [Fact]
        public void AltTest()
        {
            var expr = new AltExpression(new [] {
                new CharExpression('x'),
                new CharExpression('y')
            });
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item => Assert.Null(item)
                    );
                },
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('y', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item => Assert.Null(item)
                    );
                }
            );
        }

        [Fact]
        public void GroupTest()
        {
            var expr = new GroupExpression(new [] {
                new CharExpression('x'),
                new CharExpression('y')
            });
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item =>
                        {
                            Assert.IsType<CharacterResultTree>(item);
                            Assert.Equal('y', (item as CharacterResultTree).Value);
                            Assert.Collection((item as CharacterResultTree).Next,
                                item => Assert.Null(item)
                            );
                        }
                    );
                }
            );
        }

        [Fact]
        public void ConjTest1()
        {
            var expr = new ConjExpression(
                new CharExpression('x'),
                new CharExpression('x')
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                }
            );
        }

        [Fact]
        public void ConjTest2()
        {
            var expr = new ConjExpression(
                new CharExpression('x'),
                new CharExpression('y')
            );
            var result = expr.Run();

            Assert.Empty(result);
        }

        [Fact]
        public void ConjTest3()
        {
            var expr = new ConjExpression(
                new AltExpression(new [] { new CharExpression('x'), new CharExpression('y') }),
                new AltExpression(new [] { new CharExpression('y'), new CharExpression('x') })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                },
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('y', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                }
            );
        }

        [Fact]
        public void ConjTest4()
        {
            var expr = new ConjExpression(
                new GroupExpression(new [] {
                    new AltExpression(new [] { new CharExpression('x'), new CharExpression('y') }),
                    new AltExpression(new [] { new CharExpression('y'), new CharExpression('x') }),
                }),
                new GroupExpression(new [] { new CharExpression('x'), new CharExpression('x') })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Collection((item as CharacterResultTree).Next,
                        item =>
                        {
                            Assert.IsType<CharacterResultTree>(item);
                            Assert.Equal('x', (item as CharacterResultTree).Value);
                            Assert.Collection((item as CharacterResultTree).Next,
                                item =>
                                {
                                    Assert.Null(item);
                                }
                            );
                        }
                    );
                }
            );
        }

        [Fact]
        public void ConjTest5()
        {
            var expr = new ConjExpression(
                new GroupExpression(new [] {
                    new AltExpression(new [] { new CharExpression('x'), new CharExpression('y') }),
                    new AltExpression(new [] { new CharExpression('y'), new CharExpression('x') }),
                }),
                new GroupExpression(new [] { new CharExpression('x') })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<CharacterResultTree>(item);
                    Assert.Equal('x', (item as CharacterResultTree).Value);
                    Assert.Empty((item as CharacterResultTree).Next);
                }
            );
        }
    }
}
