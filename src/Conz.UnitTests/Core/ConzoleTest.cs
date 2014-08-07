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
      mConsole.SetupSet(c => c.ForegroundColor = mConfig.ForegroundColor);
      mConsole.Setup(c => c.WriteLine("Hello World"));
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Red);
      mConzole.WriteLine("Hello World");
    }

    [SetUp]
    public void DoSetup() {
      mConfig = new ConzoleConfig();
      mConsole = Mok<IConsole>();
      mConzole = null;
    }

    private void InitConzole() {
      mConzole = new Conzole(mConsole.Object, mConfig);
    }

    private Mock<IConsole> mConsole;
    private ConzoleConfig mConfig;
    private Conzole mConzole;
  }
}