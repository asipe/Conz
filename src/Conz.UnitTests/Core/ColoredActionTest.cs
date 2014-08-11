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
    public void TestSuccessfulAction(Style @default, Style current, ConsoleColor expectedBackground, ConsoleColor expectedForeground) {
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
    public void TestActionThrowsStillResetsColor(Style @default, Style current, ConsoleColor expectedBackground, ConsoleColor expectedForeground) {
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
      yield return new TestCaseData(new Style("", ConsoleColor.Yellow, ConsoleColor.Red),
                                    null,
                                    ConsoleColor.Yellow,
                                    ConsoleColor.Red)
        .SetName("TestExecuteWithNullCurrentStyleUsesDefaultStyle");

      yield return new TestCaseData(new Style("", ConsoleColor.Yellow, ConsoleColor.Red),
                                    new Style("", null, null),
                                    ConsoleColor.Yellow,
                                    ConsoleColor.Red)
        .SetName("TestExecuteWithCurrentStyleWithNullColorAndBackgroundUsesDefaultStyle");

      yield return new TestCaseData(new Style("", ConsoleColor.Yellow, ConsoleColor.Red),
                                    new Style("", ConsoleColor.DarkRed, ConsoleColor.DarkYellow),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentStyleDefinedUsesCurrentStyle");

      yield return new TestCaseData(new Style("", ConsoleColor.Yellow, ConsoleColor.Red),
                                    new Style("", null, ConsoleColor.DarkYellow),
                                    ConsoleColor.Yellow,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentWithNoBackgroundUsesDefaultBackground");

      yield return new TestCaseData(new Style("", ConsoleColor.Yellow, ConsoleColor.Red),
                                    new Style("", ConsoleColor.DarkRed, null),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.Red)
        .SetName("TestExecuteWithCurrentWithNoColorUsesDefaultColor");

      yield return new TestCaseData(null,
                                    new Style("", ConsoleColor.DarkRed, ConsoleColor.DarkYellow),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentStyleDefinedAndNullDefaultUsesCurrentStyle");

      yield return new TestCaseData(new Style("", null, null),
                                    new Style("", ConsoleColor.DarkRed, ConsoleColor.DarkYellow),
                                    ConsoleColor.DarkRed,
                                    ConsoleColor.DarkYellow)
        .SetName("TestExecuteWithCurrentStyleDefinedAndNullDefaultValuesUsesCurrentStyle");

      yield return new TestCaseData(null,
                                    null,
                                    ConsoleColor.White,
                                    ConsoleColor.Green)
        .SetName("TestExecuteWithBothDefaultAndCurrentNullUseCurrentConsoleValues");

      yield return new TestCaseData(new Style("", null, null),
                                    new Style("", null, null),
                                    ConsoleColor.White,
                                    ConsoleColor.Green)
        .SetName("TestExecuteWithBothDefaultAndCurrentHaveNullValuesUseCurrentConsoleValues");
    }

    private void InitAction(Style @default, Style current) {
      mAction = new ColoredAction(mConsole.Object, @default, current);
    }

    private Mock<IConsole> mConsole;
    private ColoredAction mAction;
  }
}