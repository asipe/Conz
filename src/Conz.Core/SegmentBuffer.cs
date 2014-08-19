using System;
using System.Text;

namespace Conz.Core {
  public class SegmentBuffer {
    public SegmentBuffer() {
      mCollector = AddText;
    }

    public bool CanBuildSegment {
      get {return mClassSet || mTextSet;}
    }

    public Segment BuildSegment() {
      ValidateCanBuildSegment();
      var result = new Segment(GetTextOrNull(mClass, mClassSet), GetTextOrNull(mText, mTextSet));
      mClass.Clear();
      mClassSet = false;
      mText.Clear();
      mTextSet = false;
      mCollector = AddText;
      return result;
    }

    public SegmentBuffer CollectClass() {
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
      mClassSet = true;
      mClass.Append(c);
    }

    private void AddText(char c) {
      mTextSet = true;
      mText.Append(c);
    }

    private void ValidateCanBuildSegment() {
      if (!CanBuildSegment)
        throw new ConzException("Cannot Build Segment");
    }

    private readonly StringBuilder mClass = new StringBuilder();
    private readonly StringBuilder mText = new StringBuilder();
    private bool mClassSet;
    private Action<char> mCollector;
    private bool mTextSet;
  }
}