using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class StyleSheetTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var def = CA<Style>();
      var styles = CM<Style>(3);
      var sheet = new StyleSheet(def, styles);
      Assert.That(sheet.Default, Is.EqualTo(def));
      Assert.That(sheet.Styles, Is.EqualTo(styles));
    }
  }
}