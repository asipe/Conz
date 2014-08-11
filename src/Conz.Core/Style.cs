using System;

namespace Conz.Core {
  public class Style {
    public Style() {}

    public Style(string name, ConsoleColor? backgroundColor, ConsoleColor? color) {
      Name = name;
      BackgroundColor = backgroundColor;
      Color = color;
    }

    public string Name{get;set;}
    public ConsoleColor? BackgroundColor{get;set;}
    public ConsoleColor? Color{get;set;}
  }
}