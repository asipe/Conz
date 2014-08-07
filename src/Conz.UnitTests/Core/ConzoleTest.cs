using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ConzoleTest : BaseTestCase {
    [Test]
    public void TestWriteLineUsage() {
      var conz = new Conzole(mConsole.Object);
      mConsole.Setup(c => c.WriteLine("Hello World"));
      conz.WriteLine("Hello World");
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
    }

    private Mock<IConsole> mConsole;
  }
}