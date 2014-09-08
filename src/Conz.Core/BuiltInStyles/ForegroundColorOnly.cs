namespace Conz.Core.BuiltInStyles {
  public static class ForegroundColorOnly {
    static ForegroundColorOnly() {
      _Instance = new StyleSheet(new Class("default"),
                                 new Class("bl", ConzoleColor.Default, ConzoleColor.Black),
                                 new Class("db", ConzoleColor.Default, ConzoleColor.DarkBlue),
                                 new Class("dg", ConzoleColor.Default, ConzoleColor.DarkGreen),
                                 new Class("dc", ConzoleColor.Default, ConzoleColor.DarkCyan),
                                 new Class("dr", ConzoleColor.Default, ConzoleColor.DarkRed),
                                 new Class("dm", ConzoleColor.Default, ConzoleColor.DarkMagenta),
                                 new Class("dy", ConzoleColor.Default, ConzoleColor.DarkYellow),
                                 new Class("gr", ConzoleColor.Default, ConzoleColor.Gray),
                                 new Class("dgr", ConzoleColor.Default, ConzoleColor.DarkGray),
                                 new Class("b", ConzoleColor.Default, ConzoleColor.Blue),
                                 new Class("g", ConzoleColor.Default, ConzoleColor.Green),
                                 new Class("c", ConzoleColor.Default, ConzoleColor.Cyan),
                                 new Class("r", ConzoleColor.Default, ConzoleColor.Red),
                                 new Class("m", ConzoleColor.Default, ConzoleColor.Magenta),
                                 new Class("y", ConzoleColor.Default, ConzoleColor.Yellow),
                                 new Class("w", ConzoleColor.Default, ConzoleColor.White));
    }

    public static StyleSheet _Instance{get;private set;}
  }
}