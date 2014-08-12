using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conz.Core {
  public class Parser {
    private enum CollectingState {
      CsNone,
      CsStyle,
      CsText
    };

    public Segment[] Parse(string text) {
      if (text == "")
        return new[] { new Segment(null, "") };
      return text == null
               ? new[] { new Segment(null, "") }
               : ParseText(text).ToArray();
    }

    private static IEnumerable<Segment> ParseText(string text) {
      var buf = new StringBuilder();
      var bufSet = false;
      var style = new StringBuilder();
      var styleSet = false;
      var state = CollectingState.CsNone;
      StringBuilder collector = null;

      foreach (var c in text)
        switch (c) {
          case '|':
            switch (state) {
              case CollectingState.CsNone:
                state = CollectingState.CsStyle;
                collector = style;
                styleSet = true;
                break;
              case CollectingState.CsStyle:
                state = CollectingState.CsText;
                collector = buf;
                bufSet = true;
                break;
              default:
                state = CollectingState.CsNone;
                collector = null;
                yield return new Segment(style.ToString(), buf.ToString());
                style.Clear();
                buf.Clear();
                styleSet = false;
                bufSet = false;
                break;
            }
            break;
          default:
            var sb = collector ?? buf;
            sb.Append(c);
            bufSet = bufSet || ReferenceEquals(sb, buf);
            break;
        }

      if (styleSet || bufSet)
        yield return new Segment(styleSet ? style.ToString() : null, bufSet ? buf.ToString() : null);
    }
  }
}