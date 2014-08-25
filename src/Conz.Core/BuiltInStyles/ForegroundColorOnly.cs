using System;

namespace Conz.Core.BuiltInStyles {
  public static class ForegroundColorOnly {
    static ForegroundColorOnly() {
      _Instance = new StyleSheet(null,
                                 new Class("bl", null, ConsoleColor.Black),
                                 new Class("db", null, ConsoleColor.DarkBlue),
                                 new Class("dg", null, ConsoleColor.DarkGreen),
                                 new Class("dc", null, ConsoleColor.DarkCyan),
                                 new Class("dr", null, ConsoleColor.DarkRed),
                                 new Class("dm", null, ConsoleColor.DarkMagenta),
                                 new Class("dy", null, ConsoleColor.DarkYellow),
                                 new Class("gr", null, ConsoleColor.Gray),
                                 new Class("dgr", null, ConsoleColor.DarkGray),
                                 new Class("b", null, ConsoleColor.Blue),
                                 new Class("g", null, ConsoleColor.Green),
                                 new Class("c", null, ConsoleColor.Cyan),
                                 new Class("r", null, ConsoleColor.Red),
                                 new Class("m", null, ConsoleColor.Magenta),
                                 new Class("y", null, ConsoleColor.Yellow),
                                 new Class("w", null, ConsoleColor.White));
    }

    public static StyleSheet _Instance{get;private set;}
  }
}