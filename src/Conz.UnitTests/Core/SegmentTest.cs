using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class SegmentTest : BaseTestCase {
    [Test]
    public void TestDefaults() {
      var segment = new Segment("style", "text");
      Assert.That(segment.Style, Is.EqualTo("style"));
      Assert.That(segment.Text, Is.EqualTo("text"));
    }

    [TestCase("", false)]
    [TestCase("a", false)]
    [TestCase(null, true)]
    public void TestIsDefaultStyle(string style, bool expected) {
      Assert.That(new Segment(style, "").IsDefaultStyle, Is.EqualTo(expected));
    }
  }
}