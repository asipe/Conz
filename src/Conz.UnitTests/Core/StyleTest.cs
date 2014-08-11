using System;
using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class StyleTest : BaseTestCase {
    [Test]
    public void TestArgsCtor() {
      var style = new Style("style", ConsoleColor.Red, ConsoleColor.Green);
      Assert.That(style.Name, Is.EqualTo("style"));
      Assert.That(style.BackgroundColor, Is.EqualTo(ConsoleColor.Red));
      Assert.That(style.Color, Is.EqualTo(ConsoleColor.Green));
    }
  }
}