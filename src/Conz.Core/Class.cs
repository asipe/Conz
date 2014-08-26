using System;

namespace Conz.Core {
  public class Class {
    public Class(string name, int indent) : this(name, null, null, indent) {
      Name = name;
      Indent = indent;
    }

    public Class(string name, ConsoleColor? backgroundColor = null, ConsoleColor? color = null, int indent = 0) {
      Name = name;
      BackgroundColor = backgroundColor;
      Color = color;
      Indent = indent;
    }

    public int Indent{get;set;}
    public string Name{get;private set;}
    public ConsoleColor? BackgroundColor{get;private set;}
    public ConsoleColor? Color{get;private set;}
  }
}