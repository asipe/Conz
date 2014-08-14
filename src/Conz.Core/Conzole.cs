using System;
using System.Linq;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole {
    public Conzole(IConsole console, IParser parser, StyleSheet styleSheet) {
      mMap = new StyleSheetMap(styleSheet);
      mConsole = console;
      mParser = parser;
      mStyleSheet = styleSheet;
    }

    public void WriteLine(string value) {
      Write(value);
      mConsole.WriteLine();
    }

    public void Write(string value) {
      var work = mParser
        .Parse(value)
        .Select(segment => new {
                                 Class = mMap[segment.Class],
                                 segment.Text
                               })
        .Select(item => new {
                              Action = new ColoredAction(mConsole, mStyleSheet.Default, item.Class),
                              item.Text
                            })
        .ToArray();

      Array.ForEach(work, w => w.Action.Execute(c => c.Write(w.Text)));
    }

    private readonly IConsole mConsole;
    private readonly StyleSheetMap mMap;
    private readonly IParser mParser;
    private readonly StyleSheet mStyleSheet;
  }
}