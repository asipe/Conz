using System;
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
    public void TestWritelineWhenParserReturnsSingleEmptySegment() {
      var segments = BA(new Segment(null, "Hello World"));
      mParser.Setup(p => p.Parse("Hello World")).Returns(segments);
      mFactory
        .Setup(f => f.Build(mConsole.Object, IsEq(mStyleSheet.Default), IsEq(mStyleSheet.Default)))
        .Returns(mAction);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.WriteLine());
      mConzole.WriteLine("Hello World");
      mFactory.Verify(f => f.Build(mConsole.Object, It.IsAny<Class>(), It.IsAny<Class>()), Times.Once());
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mParser = Mok<IParser>();
      mFactory = Mok<IColoredActionFactory>();
      mStyleSheet = new StyleSheet(new Class("default", ConsoleColor.Yellow, ConsoleColor.White),
                                   BA(new Class("blackonblue", ConsoleColor.Blue, ConsoleColor.Black)));
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