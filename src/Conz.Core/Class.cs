using System;

namespace Conz.Core {
  public class Class {
    public Class(string name, ConsoleColor? backgroundColor, ConsoleColor? color) {
      Name = name;
      BackgroundColor = backgroundColor;
      Color = color;
    }

    public string Name{get;private set;}
    public ConsoleColor? BackgroundColor{get;private set;}
    public ConsoleColor? Color{get;private set;}
  }
}