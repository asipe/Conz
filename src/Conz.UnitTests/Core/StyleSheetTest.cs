using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class StyleSheetTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var sheet = new StyleSheet();
      Assert.That(sheet.Default, Is.Null);
      Assert.That(sheet.Styles, Is.Null);
    }
  }
}