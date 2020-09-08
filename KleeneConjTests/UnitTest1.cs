using System;
using KleeneConj;
using Xunit;

namespace KleeneConjTests
{
    public class UnitTest1
    {
        [Fact]
        public void StructureTest()
        {
            var expr = new StructureExpression("foo");
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item => Assert.Null(item)
                    );
                }
            );
        }

        [Fact]
        public void StructureTestWithChild()
        {
            var expr = new StructureExpression("foo", new StructureExpression("bar"));
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("bar", (item as StructureResultTree).Name);
                            Assert.Collection((item as StructureResultTree).FirstChild,
                                item => Assert.Null(item)
                            );
                            Assert.Collection((item as StructureResultTree).NextSibling,
                                item => Assert.Null(item)
                            );
                        }
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item => Assert.Null(item)
                    );
                }
            );
        }

        [Fact]
        public void AltTest()
        {
            var expr = new AltExpression(new[] {
                new StructureExpression("foo"),
                new StructureExpression("bar")
            });
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item => Assert.Null(item)
                    );
                },
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("bar", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item => Assert.Null(item)
                    );
                }
            );
        }

        [Fact]
        public void GroupTest()
        {
            var expr = new GroupExpression(new[] {
                new StructureExpression("foo"),
                new StructureExpression("bar")
            });
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item =>
                        {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("bar", (item as StructureResultTree).Name);
                            Assert.Collection((item as StructureResultTree).FirstChild,
                                item => Assert.Null(item)
                            );
                            Assert.Collection((item as StructureResultTree).NextSibling,
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
                new StructureExpression("foo"),
                new StructureExpression("foo")
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
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
                new StructureExpression("foo"),
                new StructureExpression("bar")
            );
            var result = expr.Run();

            Assert.Empty(result);
        }

        [Fact]
        public void ConjTest3()
        {
            var expr = new ConjExpression(
                new AltExpression(new[] { new StructureExpression("foo"), new StructureExpression("bar") }),
                new AltExpression(new[] { new StructureExpression("bar"), new StructureExpression("foo") })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item =>
                        {
                            Assert.Null(item);
                        }
                    );
                },
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("bar", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
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
                new GroupExpression(new[] {
                    new AltExpression(new [] { new StructureExpression("foo"), new StructureExpression("bar") }),
                    new AltExpression(new [] { new StructureExpression("bar"), new StructureExpression("foo") }),
                }),
                new GroupExpression(new[] { new StructureExpression("foo"), new StructureExpression("foo") })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item =>
                        {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("foo", (item as StructureResultTree).Name);
                            Assert.Collection((item as StructureResultTree).FirstChild,
                                item => Assert.Null(item)
                            );
                            Assert.Collection((item as StructureResultTree).NextSibling,
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
                new GroupExpression(new[] {
                    new AltExpression(new [] { new StructureExpression("foo"), new StructureExpression("bar") }),
                    new AltExpression(new [] { new StructureExpression("bar"), new StructureExpression("foo") }),
                }),
                new GroupExpression(new[] { new StructureExpression("foo") })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => Assert.Null(item)
                    );
                    Assert.Empty((item as StructureResultTree).NextSibling);
                }
            );
        }

        [Fact]
        public void ConjTest6()
        {
            var expr = new ConjExpression(
                new GroupExpression(new[] {
                    new AltExpression(new [] { new StructureExpression("foo", new StructureExpression("xxx")), new StructureExpression("foo") }),
                    new AltExpression(new [] { new StructureExpression("foo", new StructureExpression("zzz")), new StructureExpression("foo", new StructureExpression("yyy")) }),
                }),
                new GroupExpression(new[] { new StructureExpression("foo", new StructureExpression("xxx")), new StructureExpression("foo", new StructureExpression("yyy")) })
            );
            var result = expr.Run();

            Assert.Collection(result,
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Collection((item as StructureResultTree).FirstChild,
                        item => {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("xxx", (item as StructureResultTree).Name);
                            Assert.Collection((item as StructureResultTree).FirstChild,
                                item => Assert.Null(item)
                            );
                            Assert.Collection((item as StructureResultTree).NextSibling,
                                item => Assert.Null(item)
                            );
                        }
                    );
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item =>
                        {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("foo", (item as StructureResultTree).Name);
                            Assert.Empty((item as StructureResultTree).FirstChild);
                            Assert.Collection((item as StructureResultTree).NextSibling,
                                item =>
                                {
                                    Assert.Null(item);
                                }
                            );
                        },
                        item =>
                        {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("foo", (item as StructureResultTree).Name);
                            Assert.Collection((item as StructureResultTree).FirstChild,
                                item => {
                                    Assert.IsType<StructureResultTree>(item);
                                    Assert.Equal("yyy", (item as StructureResultTree).Name);
                                    Assert.Collection((item as StructureResultTree).FirstChild,
                                        item => Assert.Null(item)
                                    );
                                    Assert.Collection((item as StructureResultTree).NextSibling,
                                        item => Assert.Null(item)
                                    );
                                }
                            );
                            Assert.Collection((item as StructureResultTree).NextSibling,
                                item => Assert.Null(item)
                            );
                        }
                    );
                },
                item =>
                {
                    Assert.IsType<StructureResultTree>(item);
                    Assert.Equal("foo", (item as StructureResultTree).Name);
                    Assert.Empty((item as StructureResultTree).FirstChild);
                    Assert.Collection((item as StructureResultTree).NextSibling,
                        item =>
                        {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("foo", (item as StructureResultTree).Name);
                            Assert.Empty((item as StructureResultTree).FirstChild);
                            Assert.Collection((item as StructureResultTree).NextSibling,
                                item =>
                                {
                                    Assert.Null(item);
                                }
                            );
                        },
                        item =>
                        {
                            Assert.IsType<StructureResultTree>(item);
                            Assert.Equal("foo", (item as StructureResultTree).Name);
                            Assert.Collection((item as StructureResultTree).FirstChild,
                                item => {
                                    Assert.IsType<StructureResultTree>(item);
                                    Assert.Equal("yyy", (item as StructureResultTree).Name);
                                    Assert.Collection((item as StructureResultTree).FirstChild,
                                        item => Assert.Null(item)
                                    );
                                    Assert.Collection((item as StructureResultTree).NextSibling,
                                        item => Assert.Null(item)
                                    );
                                }
                            );
                            Assert.Collection((item as StructureResultTree).NextSibling,
                                item => Assert.Null(item)
                            );
                        }
                    );
                }
            );
        }
    }
}
