using System.Linq;
using Conz.Core.BuiltInStyles;
using NUnit.Framework;

namespace Conz.UnitTests.Core.BuiltInStyles {
  [TestFixture]
  public class ForegroundColorOnlyTest : BaseTestCase {
    [Test]
    public void TestNamesAreUnique() {
      var distinctNames = ForegroundColorOnly
        ._Instance
        .Classes
        .Select(c => c.Name)
        .Distinct();
      Assert.That(ForegroundColorOnly._Instance.Classes.Length, Is.EqualTo(distinctNames.Count()));
    }
  }
}