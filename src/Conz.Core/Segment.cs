namespace Conz.Core {
  public class Segment {
    public Segment(string @class, string text) {
      Class = @class;
      Text = text;
      IsDefaultStyle = @class == null;
    }

    public string Class{get;private set;}
    public string Text{get;private set;}
    public bool IsDefaultStyle{get;private set;}
  }
}