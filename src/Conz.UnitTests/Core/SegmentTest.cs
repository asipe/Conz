using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class SegmentTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var segment = new Segment("style", "text");
      Assert.That(segment.Class, Is.EqualTo("style"));
      Assert.That(segment.Text, Is.EqualTo("text"));
    }
  }
}