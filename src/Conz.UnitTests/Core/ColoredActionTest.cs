using System;
using System.Collections;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ColoredActionTest : BaseTestCase {
    [TestCaseSource("GetActionTests")]
    public void TestSuccessfulAction(Class @default, Class current, ConsoleColor expectedBackground, ConsoleColor expectedForeground) {
      InitAction(@default, current);
      mConsole.Setup(c => c.ForegroundColor).Returns(ConsoleColor.Green);
      mConsole.Setup(c => c.BackgroundColor).Returns(ConsoleColor.White);
      mConsole.SetupSet(c => c.ForegroundColor = expectedForeground);
      mConsole.SetupSet(c => c.BackgroundColor = expectedBackground);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Green);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.White);
      IConsole console = null;
      mAction.Execute(c => console = c);
      Assert.That(console, Is.EqualTo(mConsole.Object));
    }

    [TestCaseSource("GetActionTests")]
    public void TestActionThrowsStillResetsColor(Class @default, Class current, ConsoleColor expectedBackground, ConsoleColor expectedForeground) {
      InitAction(@default, current);
      mConsole.Setup(c => c.ForegroundColor).Returns(ConsoleColor.Green);
      mConsole.Setup(c => c.BackgroundColor).Returns(ConsoleColor.White);
      mConsole.SetupSet(c => c.ForegroundColor = expectedForeground);
      mConsole.SetupSet(c => c.BackgroundColor = expectedBackground);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Green);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.White);
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
      mAction = null;
    }

    private static IEnumerable GetActionTests() {
      yield return new TestCaseData(new Class("", ConzoleColor.Yellow, ConzoleColor.Red),
                                    null,
                                    ConsoleColor.Yellow,
                                    ConsoleColor.Red)
        .SetName("TestExecuteWithNullCurrentClassUsesDefaultClass");

      yield return new TestCaseData(new Class("", ConzoleColor.Yellow, ConzoleColor.Red),
                                    new Class(""),
                                    ConsoleColor.Yellow,
                                    ConsoleColor.Red)
        .SetName("TestExecuteWithCurrentClassWithNullColorAndBackgroundUsesDefaultClass");

      yield return new TestCaseData(new Class("", ConzoleColor.Yellow, ConzoleColor.Red),
                                    new Class("", ConzoleColor.DarkRed, ConzoleColor.DarkYellow),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentClassDefinedUsesCurrentClass");

      yield return new TestCaseData(new Class("", ConzoleColor.Yellow, ConzoleColor.Red),
                                    new Class("", ConzoleColor.Default, ConzoleColor.DarkYellow),
                                    ConsoleColor.Yellow,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentWithNoBackgroundUsesDefaultBackground");

      yield return new TestCaseData(new Class("", ConzoleColor.Yellow, ConzoleColor.Red),
                                    new Class("", ConzoleColor.DarkRed),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.Red)
        .SetName("TestExecuteWithCurrentWithNoColorUsesDefaultColor");

      yield return new TestCaseData(null,
                                    new Class("", ConzoleColor.DarkRed, ConzoleColor.DarkYellow),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentClassDefinedAndNullDefaultUsesCurrentClass");

      yield return new TestCaseData(new Class(""),
                                    new Class("", ConzoleColor.DarkRed, ConzoleColor.DarkYellow),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentClassDefinedAndNullDefaultValuesUsesCurrentClass");

      yield return new TestCaseData(null,
                                    null,
                                    ConsoleColor.White,
                                    ConsoleColor.Green)
        .SetName("TestExecuteWithBothDefaultAndCurrentNullUseCurrentConsoleValues");

      yield return new TestCaseData(new Class(""),
                                    new Class(""),
                                    ConsoleColor.White,
                                    ConsoleColor.Green)
        .SetName("TestExecuteWithBothDefaultAndCurrentHaveNullValuesUseCurrentConsoleValues");
    }

    private void InitAction(Class @default, Class current) {
      mAction = new ColoredAction(mConsole.Object, @default, current);
    }

    private Mock<IConsole> mConsole;
    private ColoredAction mAction;
  }
}