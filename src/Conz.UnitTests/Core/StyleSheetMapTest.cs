using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class StyleSheetMapTest : BaseTestCase {
    [Test]
    public void TestGetForNameWhenClassesEmptyGivesDefault() {
      var @default = CA<Class>();
      var map = new StyleSheetMap(new StyleSheet(@default));
      Assert.That(map[CA<string>()], Is.EqualTo(@default));
    }

    [Test]
    public void TestGetForNameWhenNotFoundWithSingleClassGivesDefault() {
      var @default = CA<Class>();
      var map = new StyleSheetMap(new StyleSheet(@default, CM<Class>(1)));
      Assert.That(map[CA<string>()], Is.EqualTo(@default));
    }

    [Test]
    public void TestGetForNameWhenNotFoundWithMultipleClassesGivesDefault() {
      var @default = CA<Class>();
      var map = new StyleSheetMap(new StyleSheet(@default, CM<Class>(3)));
      Assert.That(map[CA<string>()], Is.EqualTo(@default));
    }

    [Test]
    public void TestGetForNameWhenFoundWithSingleClassGivesClass() {
      var classes = CM<Class>(1);
      var map = new StyleSheetMap(new StyleSheet(CA<Class>(), classes));
      Assert.That(map[classes[0].Name], Is.EqualTo(classes[0]));
    }

    [Test]
    public void TestGetForNameWhenFoundWithMultipleClassesGivesClass() {
      var classes = CM<Class>(3);
      var map = new StyleSheetMap(new StyleSheet(CA<Class>(), classes));
      Assert.That(map[classes[0].Name], Is.EqualTo(classes[0]));
      Assert.That(map[classes[2].Name], Is.EqualTo(classes[2]));
    }

    [Test]
    public void TestGetForNameWhenNameNullGivesDefault() {
      var @default = CA<Class>();
      var map = new StyleSheetMap(new StyleSheet(@default, CM<Class>(0)));
      Assert.That(map[null], Is.EqualTo(@default));
    }
  }
}