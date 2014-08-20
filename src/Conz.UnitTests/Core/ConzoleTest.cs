using System;
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
      mParser.Setup(p => p.Parse("|blackonblue|Hello World|")).Returns(BA(new Segment("blackonblue", "Hello World")));
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Classes[0])))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("|blackonblue|Hello World|");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [Test]
    public void TestWriteLineWithMultipleSegments() {
      mParser.Setup(p => p.Parse("|blackonblue|Hello World||dark| ||redongreen|Goodbye|end")).Returns(BA(new Segment("blackonblue", "Hello World"),
                                                                                                         new Segment("dark", " "),
                                                                                                         new Segment("redongreen", "Goodbye"),
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
      mConzole.WriteLine("|blackonblue|Hello World||dark| ||redongreen|Goodbye|end");
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

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mParser = Mok<IParser>();
      mFactory = Mok<IColoredActionFactory>();
      mStyleSheet = new StyleSheet(new Class("default", ConsoleColor.Yellow, ConsoleColor.White),
                                   BA(new Class("blackonblue", ConsoleColor.Blue, ConsoleColor.Black),
                                      new Class("redongreen", ConsoleColor.Green, ConsoleColor.Red),
                                      new Class("dark", ConsoleColor.DarkBlue, ConsoleColor.DarkCyan)));
      mAction = new StubColoredAction(mConsole.Object);
      mConzole = new Conzole(mConsole.Object, mParser.Object, mFactory.Object, mStyleSheet);
    }

    private Mock<IConsole> mConsole;
    private Conzole mConzole;
    private StyleSheet mStyleSheet;
    private Mock<IParser> mParser;
    private Mock<IColoredActionFactory> mFactory;
    private StubColoredAction mAction;
  }
}