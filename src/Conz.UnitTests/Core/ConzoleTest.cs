using System;
using System.Collections.Generic;
using System.IO;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ConzoleTest : BaseTestCase {
    private sealed class StubColoredAction : IColoredAction {
      public StubColoredAction(IConsole console) {
        mConsole = console;
      }

      public void Execute(Action<IConsole> action) {
        action.Invoke(mConsole);
      }

      private readonly IConsole mConsole;
    }

    [Test]
    public void TestWriteLineWithNoSegments() {
      mParser.Setup(p => p.Parse("")).Returns(BA<Segment>());
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("");
    }

    [Test]
    public void TestWriteLineWithSingleSegmentWithNoClass() {
      mParser.Setup(p => p.Parse("Hello World")).Returns(BA(new Segment(null, "Hello World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("Hello World");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineWithSingleSegmentWithClass() {
      mParser.Setup(p => p.Parse("|f1|Hello World|")).Returns(BA(new Segment("f1", "Hello World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("|f1|Hello World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineWithMultipleSegments() {
      mParser.Setup(p => p.Parse("|f1|Hello World||f3| ||f2|Goodbye|end")).Returns(BA(new Segment("f1", "Hello World"),
                                                                                      new Segment("f3", " "),
                                                                                      new Segment("f2", "Goodbye"),
                                                                                      new Segment(null, "end")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[2])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.Write(" "));
      mConsole.Setup(c => c.Write("Goodbye"));
      mConsole.Setup(c => c.Write("end"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("|f1|Hello World||f3| ||f2|Goodbye|end");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(4));
    }

    [Test]
    public void TestErrorDelegates() {
      using (var tw = new StringWriter()) {
        mConsole.Setup(c => c.Error).Returns(tw);
        Assert.That(mConzole.Error, Is.EqualTo(tw));
      }
    }

    [Test]
    public void TestInDelegates() {
      using (var sw = new StringReader("")) {
        mConsole.Setup(c => c.In).Returns(sw);
        Assert.That(mConzole.In, Is.EqualTo(sw));
      }
    }

    [Test]
    public void TestOutDelegates() {
      using (var tw = new StringWriter()) {
        mConsole.Setup(c => c.Out).Returns(tw);
        Assert.That(mConzole.Out, Is.EqualTo(tw));
      }
    }

    [Test]
    public void TestBackgroundColorDelegates() {
      mConsole.Setup(c => c.BackgroundColor).Returns(ConsoleColor.Red);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.Green);
      Assert.That(mConzole.BackgroundColor, Is.EqualTo(ConsoleColor.Red));
      mConzole.BackgroundColor = ConsoleColor.Green;
    }

    [Test]
    public void TestForegroundColorDelegates() {
      mConsole.Setup(c => c.ForegroundColor).Returns(ConsoleColor.Red);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Green);
      Assert.That(mConzole.ForegroundColor, Is.EqualTo(ConsoleColor.Red));
      mConzole.ForegroundColor = ConsoleColor.Green;
    }

    [Test]
    public void TestReadLineDelegates() {
      mConsole.Setup(c => c.ReadLine()).Returns("avalue");
      Assert.That(mConzole.ReadLine(), Is.EqualTo("avalue"));
    }

    [Test]
    public void TestReadKeyDelegates() {
      var info = new ConsoleKeyInfo();
      mConsole.Setup(c => c.ReadKey()).Returns(info);
      Assert.That(mConzole.ReadKey(), Is.EqualTo(info));
    }

    [Test]
    public void TestResetColorDelegates() {
      mConsole.Setup(c => c.ResetColor());
      mConzole.ResetColor();
    }

    [Test]
    public void TestReadKeyWithInterceptDelegates() {
      var info = new ConsoleKeyInfo();
      mConsole.Setup(c => c.ReadKey(true)).Returns(info);
      Assert.That(mConzole.ReadKey(true), Is.EqualTo(info));
      mConsole.Setup(c => c.ReadKey(false)).Returns(info);
      Assert.That(mConzole.ReadKey(false), Is.EqualTo(info));
    }

    [Test]
    public void TestWriteLineWithNoArgs() {
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine();
    }

    [Test]
    public void TestWriteBool() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(true));
      mConzole.Write(true);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteChar() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write('a'));
      mConzole.Write('a');
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteCharArray() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(new[] {'a', 'b'}));
      mConzole.Write(new[] {'a', 'b'});
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteDecimal() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(1m));
      mConzole.Write(1m);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteDouble() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(1.5));
      mConzole.Write(1.5);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteInt() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(1));
      mConzole.Write(1);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLong() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(1L));
      mConzole.Write(1L);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteFloat() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(1f));
      mConzole.Write(1f);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteUInt() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write((uint)1));
      mConzole.Write((uint)1);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteULong() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write((ulong)1));
      mConzole.Write((ulong)1);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteObject() {
      var list = new List<int>();
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(list));
      mConzole.Write(list);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteRangedCharBuffer() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write(new[] {'a'}, 1, 2));
      mConzole.Write(new[] {'a'}, 1, 2);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineBool() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(true));
      mConzole.WriteLine(true);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineChar() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine('a'));
      mConzole.WriteLine('a');
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteFormattedSingleArgNoFormat() {
      mParser.Setup(p => p.Parse("Hello World")).Returns(BA(new Segment(null, "Hello World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConzole.Write("Hello {0}", "World");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteFormattedSingleArgWithFormat() {
      mParser.Setup(p => p.Parse("Hello |f1|World|")).Returns(BA(new Segment(null, "Hello "),
                                                                 new Segment("f1", "World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello "));
      mConsole.Setup(c => c.Write("World"));
      mConzole.Write("Hello {0}", "|f1|World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(2));
    }

    [Test]
    public void TestWriteFormatted2ArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello| |f1|World|")).Returns(BA(new Segment("f2", "Hello"),
                                                                      new Segment(null, " "),
                                                                      new Segment("f1", "World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write(" "));
      mConsole.Setup(c => c.Write("World"));
      mConzole.Write("{0} {1}", "|f2|Hello|", "|f1|World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(3));
    }

    [Test]
    public void TestWriteFormatted3ArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello|-|f1|World|")).Returns(BA(new Segment("f2", "Hello"),
                                                                      new Segment(null, "-"),
                                                                      new Segment("f1", "World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write("-"));
      mConsole.Setup(c => c.Write("World"));
      mConzole.Write("{0}{1}{2}", "|f2|Hello|", "-", "|f1|World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(3));
    }

    [Test]
    public void TestWriteFormatted4ArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello|-|f1|World|*")).Returns(BA(new Segment("f2", "Hello"),
                                                                       new Segment(null, "-"),
                                                                       new Segment("f1", "World"),
                                                                       new Segment(null, "*")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write("-"));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.Write("*"));
      mConzole.Write("{0}{1}{2}{3}", "|f2|Hello|", "-", "|f1|World|", "*");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(4));
    }

    [Test]
    public void TestWriteFormattedParamsArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello|-|f1|World|*|f3|_|")).Returns(BA(new Segment("f2", "Hello"),
                                                                             new Segment(null, "-"),
                                                                             new Segment("f1", "World"),
                                                                             new Segment(null, "*"),
                                                                             new Segment("f3", "_")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[2])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write("-"));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.Write("*"));
      mConsole.Setup(c => c.Write("_"));
      mConzole.Write("{0}{1}{2}{3}{4}", "|f2|Hello|", "-", "|f1|World|", "*", "|f3|_|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(5));
    }

    [Test]
    public void TestWriteLineCharArray() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(new[] {'a', 'b'}));
      mConzole.WriteLine(new[] {'a', 'b'});
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineDecimal() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(1m));
      mConzole.WriteLine(1m);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineDouble() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(1.5));
      mConzole.WriteLine(1.5);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineInt() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(1));
      mConzole.WriteLine(1);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineLong() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(1L));
      mConzole.WriteLine(1L);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineFloat() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(1f));
      mConzole.WriteLine(1f);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineUInt() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine((uint)1));
      mConzole.WriteLine((uint)1);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineULong() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine((ulong)1));
      mConzole.WriteLine((ulong)1);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineObject() {
      var list = new List<int>();
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(list));
      mConzole.WriteLine(list);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineRangedCharBuffer() {
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.WriteLine(new[] {'a'}, 1, 2));
      mConzole.WriteLine(new[] {'a'}, 1, 2);
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineFormattedSingleArgNoFormat() {
      mParser.Setup(p => p.Parse("Hello World")).Returns(BA(new Segment(null, "Hello World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("Hello {0}", "World");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineFormattedSingleArgWithFormat() {
      mParser.Setup(p => p.Parse("Hello |f1|World|")).Returns(BA(new Segment(null, "Hello "),
                                                                 new Segment("f1", "World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello "));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("Hello {0}", "|f1|World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(2));
    }

    [Test]
    public void TestWriteLineFormatted2ArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello| |f1|World|")).Returns(BA(new Segment("f2", "Hello"),
                                                                      new Segment(null, " "),
                                                                      new Segment("f1", "World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write(" "));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("{0} {1}", "|f2|Hello|", "|f1|World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(3));
    }

    [Test]
    public void TestWriteLineFormatted3ArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello|-|f1|World|")).Returns(BA(new Segment("f2", "Hello"),
                                                                      new Segment(null, "-"),
                                                                      new Segment("f1", "World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write("-"));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("{0}{1}{2}", "|f2|Hello|", "-", "|f1|World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(3));
    }

    [Test]
    public void TestWriteLineFormatted4ArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello|-|f1|World|*")).Returns(BA(new Segment("f2", "Hello"),
                                                                       new Segment(null, "-"),
                                                                       new Segment("f1", "World"),
                                                                       new Segment(null, "*")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write("-"));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.Write("*"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("{0}{1}{2}{3}", "|f2|Hello|", "-", "|f1|World|", "*");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(4));
    }

    [Test]
    public void TestWriteLineFormattedParamsArgWithFormat() {
      mParser.Setup(p => p.Parse("|f2|Hello|-|f1|World|*|f3|_|")).Returns(BA(new Segment("f2", "Hello"),
                                                                             new Segment(null, "-"),
                                                                             new Segment("f1", "World"),
                                                                             new Segment(null, "*"),
                                                                             new Segment("f3", "_")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[1])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[2])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello"));
      mConsole.Setup(c => c.Write("-"));
      mConsole.Setup(c => c.Write("World"));
      mConsole.Setup(c => c.Write("*"));
      mConsole.Setup(c => c.Write("_"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("{0}{1}{2}{3}{4}", "|f2|Hello|", "-", "|f1|World|", "*", "|f3|_|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Exactly(5));
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mParser = Mok<IParser>();
      mFactory = Mok<IColoredActionFactory>();
      mStyleSheet = new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.White),
                                   BA(new Class("f1", ConzoleColor.Blue, ConzoleColor.Black),
                                      new Class("f2", ConzoleColor.Green, ConzoleColor.Red),
                                      new Class("f3", ConzoleColor.DarkBlue, ConzoleColor.DarkCyan)));
      mAction = new StubColoredAction(mConsole.Object);
      mConzole = new Conzole(mStyleSheet, mConsole.Object, mParser.Object, mFactory.Object);
    }

    private Mock<IConsole> mConsole;
    private Conzole mConzole;
    private StyleSheet mStyleSheet;
    private Mock<IParser> mParser;
    private Mock<IColoredActionFactory> mFactory;
    private StubColoredAction mAction;
  }
}