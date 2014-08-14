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

    [Test]
    public void TestNullClassesInCtorSetsToEmptyClasses() {
      Assert.That(new StyleSheet(CA<Class>(), null).Classes, Is.EqualTo(Constants._EmptyClasses));
    }

    [Test]
    public void TestNullDefaultInCtorSetsToEmptyClass() {
      Assert.That(new StyleSheet(null, CM<Class>(1)).Default, Is.EqualTo(Constants._EmptyDefaultClass));
    }
  }
}