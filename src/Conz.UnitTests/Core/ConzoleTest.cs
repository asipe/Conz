using System;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ConzoleTest : BaseTestCase {
    [Test]
    public void TestWriteLineUsage() {
      InitConzole();
      mConsole.SetupGet(c => c.ForegroundColor).Returns(ConsoleColor.Red);
      mConsole.SetupGet(c => c.BackgroundColor).Returns(ConsoleColor.Green);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.White);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.Yellow);
      mConsole.Setup(c => c.WriteLine("Hello World"));
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Red);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.Green);
      mConzole.WriteLine("Hello World");
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mConfig = new ConzoleConfig {
                                    ForegroundColor = ConsoleColor.White,
                                    BackgroundColor = ConsoleColor.Yellow,
                                    Console = mConsole.Object
                                  };
      mConzole = null;
    }

    private void InitConzole() {
      mConzole = new Conzole(mConfig);
    }

    private Mock<IConsole> mConsole;
    private ConzoleConfig mConfig;
    private Conzole mConzole;
  }
}