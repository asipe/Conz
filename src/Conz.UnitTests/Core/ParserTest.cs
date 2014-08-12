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

    [Test]
    public void TestParseWithTextStyled() {
      var actual = mParser.Parse("|a|hello world|");
      Assert.That(actual.Length, Is.EqualTo(1));
      Assert.That(actual[0].Style, Is.EqualTo("a"));
      Assert.That(actual[0].Text, Is.EqualTo("hello world"));      
    }

    [SetUp]
    public void DoSetup() {
      mParser = new Parser();
    }

    private Parser mParser;
  }
}