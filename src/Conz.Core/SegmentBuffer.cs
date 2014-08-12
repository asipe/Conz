using System;
using System.Text;

namespace Conz.Core {
  public class SegmentBuffer {
    public SegmentBuffer() {
      mCollector = AddText;
    }

    public bool CanBuildSegment {
      get {return mStyleSet || mTextSet;}
    }

    public Segment BuildSegment() {
      ValidateCanBuildSegment();
      var result = new Segment(GetTextOrNull(mStyle, mStyleSet), GetTextOrNull(mText, mTextSet));
      mStyle.Clear();
      mStyleSet = false;
      mText.Clear();
      mTextSet = false;
      mCollector = AddText;
      return result;
    }

    public SegmentBuffer CollectStyle() {
      mCollector = AddStyle;
      return this;
    }

    public SegmentBuffer CollectText() {
      mCollector = AddText;
      return this;
    }

    public SegmentBuffer Add(char c) {
      mCollector.Invoke(c);
      return this;
    }

    private static string GetTextOrNull(StringBuilder buf, bool isSet) {
      return isSet ? buf.ToString() : null;
    }

    private void AddStyle(char c) {
      mStyleSet = true;
      mStyle.Append(c);
    }

    private void AddText(char c) {
      mTextSet = true;
      mText.Append(c);
    }

    private void ValidateCanBuildSegment() {
      if (!CanBuildSegment)
        throw new Exception("Cannot Build Segment");
    }

    private readonly StringBuilder mStyle = new StringBuilder();
    private readonly StringBuilder mText = new StringBuilder();
    private Action<char> mCollector;
    private bool mStyleSet;
    private bool mTextSet;
  }
}