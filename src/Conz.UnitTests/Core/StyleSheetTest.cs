using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class StyleSheetTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var def = new Style(null, null, null);
      var styles = new Style[3];
      var sheet = new StyleSheet(def, styles);
      Assert.That(sheet.Default, Is.EqualTo(def));
      Assert.That(sheet.Styles, Is.EqualTo(styles));
    }
  }
}