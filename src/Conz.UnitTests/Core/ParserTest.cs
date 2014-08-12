using System.Collections;
using Conz.Core;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ParserTest : BaseTestCase {
    [TestCaseSource("GetParseTests")]
    public void TestParseWithTextStyled(string text, Segment[] expected) {
      AssertAreEqual(mParser.Parse(text), expected);
    }

    [SetUp]
    public void DoSetup() {
      mParser = new Parser();
    }

    private IEnumerable GetParseTests() {
      yield return new TestCaseData(null, BA(new Segment(null, "")));
      yield return new TestCaseData("", BA(new Segment(null, "")));
      yield return new TestCaseData(" ", BA(new Segment(null, " ")));
      yield return new TestCaseData("  ", BA(new Segment(null, "  ")));
      yield return new TestCaseData("a", BA(new Segment(null, "a")));
      yield return new TestCaseData("abc", BA(new Segment(null, "abc")));
      yield return new TestCaseData("abc\r\n", BA(new Segment(null, "abc\r\n")));
      yield return new TestCaseData("abc\r\ndef", BA(new Segment(null, "abc\r\ndef")));
      yield return new TestCaseData("|a|hello world|", BA(new Segment("a", "hello world")));
      yield return new TestCaseData("|a|hello world||b|goodbye world|", BA(new Segment("a", "hello world"),
                                                                           new Segment("b", "goodbye world")));
      yield return new TestCaseData("|a|hello world| |b|goodbye world|", BA(new Segment("a", "hello world"),
                                                                            new Segment(null, " "),
                                                                            new Segment("b", "goodbye world")));
      yield return new TestCaseData("|a|hello world|zzz|b|goodbye world|", BA(new Segment("a", "hello world"),
                                                                              new Segment(null, "zzz"),
                                                                              new Segment("b", "goodbye world")));
    }

    private Parser mParser;
  }
}