using System;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ColoredActionTest : BaseTestCase {
    [Test]
    public void TestSuccessfulAction() {
      mConsole.Setup(c => c.ForegroundColor).Returns(ConsoleColor.Green);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Red);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Green);
      IConsole console = null;
      mAction.Execute(c => console = c);
      Assert.That(console, Is.EqualTo(mConsole.Object));
    }

    [Test]
    public void TestActionThrowsStillResetsColor() {
      mConsole.Setup(c => c.ForegroundColor).Returns(ConsoleColor.Green);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Red);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Green);
      IConsole console = null;
      var ex = Assert.Throws<Exception>(() => mAction.Execute(c => {
                                                                console = c;
                                                                throw new Exception("Test Error");
                                                              }));
      Assert.That(ex.Message, Is.EqualTo("Test Error"));
      Assert.That(console, Is.EqualTo(mConsole.Object));
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mAction = new ColoredAction(mConsole.Object, ConsoleColor.Red);
    }

    private Mock<IConsole> mConsole;
    private ColoredAction mAction;
  }
}