using System.Collections.Generic;
using System.Linq;

namespace Conz.Core {
  public class Parser : IParser {
    private enum CollectingState {
      CsNone,
      CsStyle,
      CsText
    };

    public Parser(char delimiter = '|') {
      mDelimiter = delimiter;
    }

    public Segment[] Parse(string text) {
      return (text == "")
               ? _EmptySegment
               : (text == null)
                   ? _EmptySegment
                   : ParseText(text).ToArray();
    }

    private IEnumerable<Segment> ParseText(string text) {
      var buffer = new SegmentBuffer();
      var state = CollectingState.CsNone;

      foreach (var c in text)
        if (c == mDelimiter)
          switch (state) {
            case CollectingState.CsNone:
              if (buffer.CanBuildSegment)
                yield return buffer.BuildSegment();
              state = CollectingState.CsStyle;
              buffer.CollectClass();
              break;
            case CollectingState.CsStyle:
              state = CollectingState.CsText;
              buffer.CollectText();
              break;
            default:
              if (buffer.CanBuildSegment)
                yield return buffer.BuildSegment();
              state = CollectingState.CsNone;
              break;
          }
        else
          buffer.Add(c);

      if (buffer.CanBuildSegment)
        yield return buffer.BuildSegment();
    }

    private static readonly Segment[] _EmptySegment = {new Segment(null, "")};
    private readonly char mDelimiter;
  }
}