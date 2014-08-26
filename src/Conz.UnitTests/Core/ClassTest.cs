using System;
using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ClassTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var @class = new Class("class", ConsoleColor.Red, ConsoleColor.Green, 0);
      Assert.That(@class.Name, Is.EqualTo("class"));
      Assert.That(@class.BackgroundColor, Is.EqualTo(ConsoleColor.Red));
      Assert.That(@class.Color, Is.EqualTo(ConsoleColor.Green));
    }
  }
}