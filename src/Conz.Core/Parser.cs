using System.Collections.Generic;
using System.Linq;

namespace Conz.Core {
  public class Parser : IParser {
    private enum CollectingState {
      CsNone,
      CsStyle,
      CsText
    };

    public Segment[] Parse(string text) {
      return (text == "")
               ? _EmptySegment
               : (text == null)
                   ? _EmptySegment
                   : ParseText(text).ToArray();
    }

    private static IEnumerable<Segment> ParseText(string text) {
      var buffer = new SegmentBuffer();
      var state = CollectingState.CsNone;

      foreach (var c in text)
        switch (c) {
          case '|':
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
            break;
          default:
            buffer.Add(c);
            break;
        }

      if (buffer.CanBuildSegment)
        yield return buffer.BuildSegment();
    }

    private static readonly Segment[] _EmptySegment = {new Segment(null, "")};
  }
}