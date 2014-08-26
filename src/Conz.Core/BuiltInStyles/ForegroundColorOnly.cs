using System;

namespace Conz.Core.BuiltInStyles {
  public static class ForegroundColorOnly {
    static ForegroundColorOnly() {
      _Instance = new StyleSheet(null,
                                 new Class("bl", null, ConsoleColor.Black, 0),
                                 new Class("db", null, ConsoleColor.DarkBlue, 0),
                                 new Class("dg", null, ConsoleColor.DarkGreen, 0),
                                 new Class("dc", null, ConsoleColor.DarkCyan, 0),
                                 new Class("dr", null, ConsoleColor.DarkRed, 0),
                                 new Class("dm", null, ConsoleColor.DarkMagenta, 0),
                                 new Class("dy", null, ConsoleColor.DarkYellow, 0),
                                 new Class("gr", null, ConsoleColor.Gray, 0),
                                 new Class("dgr", null, ConsoleColor.DarkGray, 0),
                                 new Class("b", null, ConsoleColor.Blue, 0),
                                 new Class("g", null, ConsoleColor.Green, 0),
                                 new Class("c", null, ConsoleColor.Cyan, 0),
                                 new Class("r", null, ConsoleColor.Red, 0),
                                 new Class("m", null, ConsoleColor.Magenta, 0),
                                 new Class("y", null, ConsoleColor.Yellow, 0),
                                 new Class("w", null, ConsoleColor.White, 0));
    }

    public static StyleSheet _Instance{get;private set;}
  }
}