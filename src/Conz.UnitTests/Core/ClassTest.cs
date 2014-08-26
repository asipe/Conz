using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ClassTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var @class = new Class("class", ConzoleColor.Red, ConzoleColor.Green, 5);
      Assert.That(@class.Name, Is.EqualTo("class"));
      Assert.That(@class.BackgroundColor, Is.EqualTo(ConzoleColor.Red));
      Assert.That(@class.Color, Is.EqualTo(ConzoleColor.Green));
      Assert.That(@class.Indent, Is.EqualTo(5));
    }

    [Test]
    public void TestDefaultCtorArgs() {
      var @class = new Class("class");
      Assert.That(@class.Name, Is.EqualTo("class"));
      Assert.That(@class.BackgroundColor, Is.EqualTo(ConzoleColor.Default));
      Assert.That(@class.Color, Is.EqualTo(ConzoleColor.Default));
      Assert.That(@class.Indent, Is.EqualTo(0));
    }

    [Test]
    public void TestIndentOnlyCtor() {
      var @class = new Class("class", 5);
      Assert.That(@class.Name, Is.EqualTo("class"));
      Assert.That(@class.BackgroundColor, Is.EqualTo(ConzoleColor.Default));
      Assert.That(@class.Color, Is.EqualTo(ConzoleColor.Default));
      Assert.That(@class.Indent, Is.EqualTo(5));
    }
  }
}