using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class SegmentBufferTest : BaseTestCase {
    [Test]
    public void TestCanBuildSegmentIsFalseByDefault() {
      Assert.That(mBuffer.CanBuildSegment, Is.False);
    }

    [Test]
    public void TestBuildingSegmentWhenCanBuildSegmentIsFalseThrows() {
      var ex = Assert.Throws<ConzException>(() => mBuffer.BuildSegment());
      Assert.That(ex.Message, Is.EqualTo("Cannot Build Segment"));
    }

    [Test]
    public void TestBuildingSegmentResetsToNonBuildableState() {
      mBuffer.Add('a');
      AssertAreEqual(mBuffer.BuildSegment(), new Segment(null, "a"));
      var ex = Assert.Throws<ConzException>(() => mBuffer.BuildSegment());
      Assert.That(ex.Message, Is.EqualTo("Cannot Build Segment"));
    }

    [Test]
    public void TestByDefaultAddedCharsGoToText() {
      mBuffer.Add('a');
      AssertAreEqual(mBuffer.BuildSegment(), new Segment(null, "a"));
    }

    [Test]
    public void TestAddingTextAllowsSegmentToBeBuilt() {
      mBuffer.Add('a');
      Assert.That(mBuffer.CanBuildSegment, Is.True);
    }

    [Test]
    public void TestAddingStyleAllowsSegmentToBeBuilt() {
      mBuffer
        .CollectClass()
        .Add('a');
      Assert.That(mBuffer.CanBuildSegment, Is.True);
    }

    [Test]
    public void TestBuildingSegmentResetsToText() {
      mBuffer
        .Add('t')
        .CollectClass()
        .Add('c');
      AssertAreEqual(mBuffer.BuildSegment(), new Segment("c", "t"));
      mBuffer
        .Add('a');
      AssertAreEqual(mBuffer.BuildSegment(), new Segment(null, "a"));
    }

    [Test]
    public void TestUsage() {
      mBuffer
        .CollectClass()
        .Add('a')
        .Add('b')
        .Add('c');
      Assert.That(mBuffer.CanBuildSegment, Is.True);
      AssertAreEqual(mBuffer.BuildSegment(), new Segment("abc", null));
      mBuffer
        .CollectClass()
        .Add('s')
        .CollectText()
        .Add('t');
      Assert.That(mBuffer.CanBuildSegment, Is.True);
      AssertAreEqual(mBuffer.BuildSegment(), new Segment("s", "t"));
    }

    [SetUp]
    public void DoSetup() {
      mBuffer = new SegmentBuffer();
    }

    private SegmentBuffer mBuffer;
  }
}