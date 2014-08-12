using System.Collections;
using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ParserTest : BaseTestCase {
    [Test]
    public void TestParseNullGivesEmpty() {
      var actual = mParser.Parse(null);
      Assert.That(actual.Length, Is.EqualTo(1));
      Assert.That(actual[0].Style, Is.Null);
      Assert.That(actual[0].Text, Is.EqualTo(""));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("abc")]
    [TestCase("abc\r\n")]
    [TestCase("abc\r\ndef")]
    public void TestParseNothingStyled(string text) {
      var actual = mParser.Parse(text);
      Assert.That(actual.Length, Is.EqualTo(1));
      Assert.That(actual[0].Style, Is.Null);
      Assert.That(actual[0].Text, Is.EqualTo(text));
    }

    [TestCaseSource("GetParseTests")]
    public void TestParseWithTextStyled(string text, Segment[] expected) {
      AssertAreEqual(mParser.Parse(text), expected);
    }

    [SetUp]
    public void DoSetup() {
      mParser = new Parser();
    }

    private IEnumerable GetParseTests() {
      yield return new TestCaseData("|a|hello world|", BA(new Segment("a", "hello world")));
      yield return new TestCaseData("|a|hello world||a|hello world|", BA(new Segment("a", "hello world"),
                                                                         new Segment("a", "hello world")));
    }

    private Parser mParser;
  }
}