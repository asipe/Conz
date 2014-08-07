using System;
using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ConzoleConfigTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var config = new ConzoleConfig();
      Assert.That(config.ForegroundColor, Is.EqualTo(ConsoleColor.Black));
    } 
  }
}