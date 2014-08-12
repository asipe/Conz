using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class StyleSheetTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var def = CA<Class>();
      var styles = CM<Class>(3);
      var sheet = new StyleSheet(def, styles);
      Assert.That(sheet.Default, Is.EqualTo(def));
      Assert.That(sheet.Classes, Is.EqualTo(styles));
    }
  }
}