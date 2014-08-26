namespace Conz.Core.BuiltInStyles {
  public static class ForegroundColorOnly {
    static ForegroundColorOnly() {
      _Instance = new StyleSheet(null,
                                 new Class("bl", null, ConzoleColor.Black),
                                 new Class("db", null, ConzoleColor.DarkBlue),
                                 new Class("dg", null, ConzoleColor.DarkGreen),
                                 new Class("dc", null, ConzoleColor.DarkCyan),
                                 new Class("dr", null, ConzoleColor.DarkRed),
                                 new Class("dm", null, ConzoleColor.DarkMagenta),
                                 new Class("dy", null, ConzoleColor.DarkYellow),
                                 new Class("gr", null, ConzoleColor.Gray),
                                 new Class("dgr", null, ConzoleColor.DarkGray),
                                 new Class("b", null, ConzoleColor.Blue),
                                 new Class("g", null, ConzoleColor.Green),
                                 new Class("c", null, ConzoleColor.Cyan),
                                 new Class("r", null, ConzoleColor.Red),
                                 new Class("m", null, ConzoleColor.Magenta),
                                 new Class("y", null, ConzoleColor.Yellow),
                                 new Class("w", null, ConzoleColor.White));
    }

    public static StyleSheet _Instance{get;private set;}
  }
}