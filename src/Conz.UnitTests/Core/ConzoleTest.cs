using System;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ConzoleTest : BaseTestCase {
    [Test]
    public void TestWritelineWhenParserReturnsSingleEmptySegment() {
      var segments = BA(new Segment(null, "Hello World"));
      mParser.Setup(p => p.Parse("Hello World")).Returns(segments);
      mConsole.SetupGet(c => c.ForegroundColor).Returns(ConsoleColor.Red);
      mConsole.SetupGet(c => c.BackgroundColor).Returns(ConsoleColor.Green);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.White);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.Yellow);
      mConsole.Setup(c => c.Write("Hello World"));
      mConsole.Setup(c => c.WriteLine());
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Red);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.Green);
      mConzole.WriteLine("Hello World");
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mParser = Mok<IParser>();
      mStyleSheet = new StyleSheet(new Class("default", ConsoleColor.Yellow, ConsoleColor.White),
                                   BA(new Class("blackonblue", ConsoleColor.Blue, ConsoleColor.Black)));
      mConzole = new Conzole(mConsole.Object, mParser.Object, mStyleSheet);
    }

    private Mock<IConsole> mConsole;
    private Conzole mConzole;
    private StyleSheet mStyleSheet;
    private Mock<IParser> mParser;
  }
}