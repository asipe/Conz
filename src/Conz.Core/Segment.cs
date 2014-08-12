namespace Conz.Core {
  public class Segment {
    public Segment(string style, string text) {
      Style = style;
      Text = text;
      IsDefaultStyle = style == null;
    }

    public string Style{get;private set;}
    public string Text{get;private set;}
    public bool IsDefaultStyle{get;private set;}
  }
}