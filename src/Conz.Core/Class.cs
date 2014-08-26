namespace Conz.Core {
  public class Class {
    public Class(string name, int indent) : this(name, ConzoleColor.Default, ConzoleColor.Default, indent) {
      Name = name;
      Indent = indent;
    }

    public Class(string name, ConzoleColor backgroundColor = ConzoleColor.Default, ConzoleColor color = ConzoleColor.Default, int indent = 0) {
      Name = name;
      BackgroundColor = backgroundColor;
      Color = color;
      Indent = indent;
    }

    public int Indent{get;set;}
    public string Name{get;private set;}
    public ConzoleColor BackgroundColor{get;private set;}
    public ConzoleColor Color{get;private set;}
  }
}