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

    [Test]
    public void TestWithACustomParseCharacter() {
      mParser = new Parser('^');
      AssertAreEqual(mParser.Parse("^a^hello world^zzz^b^goodbye world^"), BA(new Segment("a", "hello world"),
                                                                              new Segment(null, "zzz"),
                                                                              new Segment("b", "goodbye world")));
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
      yield return new TestCaseData("   >|error|msg 33|  ", BA(new Segment(null, "   >"),
                                                               new Segment("error", "msg 33"),
                                                               new Segment(null, "  ")));
      yield return new TestCaseData("|a|1|\r\n|a|1|\r\n|a|1|\r\n|a|1|\r\n|a|1|\r\n", BA(new Segment("a", "1"),
                                                                                        new Segment(null, "\r\n"),
                                                                                        new Segment("a", "1"),
                                                                                        new Segment(null, "\r\n"),
                                                                                        new Segment("a", "1"),
                                                                                        new Segment(null, "\r\n"),
                                                                                        new Segment("a", "1"),
                                                                                        new Segment(null, "\r\n"),
                                                                                        new Segment("a", "1"),
                                                                                        new Segment(null, "\r\n")));
      yield return new TestCaseData("||hello world|", BA(new Segment(null, "hello world")));
      yield return new TestCaseData("|a||", BA(new Segment("a", null)));
      yield return new TestCaseData("|a|||a||", BA(new Segment("a", null),
                                                   new Segment("a", null)));
      yield return new TestCaseData("|||", BA<Segment>());
      yield return new TestCaseData("|||||||||", BA<Segment>());
    }

    private Parser mParser;
  }
}