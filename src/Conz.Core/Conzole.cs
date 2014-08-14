using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole {
    public Conzole(StyleSheet styleSheet) : this(new DotNetConsole(), new Parser(), new ColoredActionFactory(), styleSheet) {}

    public Conzole(IConsole console,
                   IParser parser,
                   IColoredActionFactory factory,
                   StyleSheet styleSheet) {
      mMap = new StyleSheetMap(styleSheet);
      mConsole = console;
      mParser = parser;
      mFactory = factory;
      mStyleSheet = styleSheet;
    }

    public void WriteLine(string value) {
      Write(value);
      mConsole.WriteLine();
    }

    public void Write(string value) {
      Array.ForEach(mParser.Parse(value), DoWork);
    }

    private void DoWork(Segment segment) {
      mFactory
        .Build(mConsole, mStyleSheet.Default, mMap[segment.Class])
        .Execute(c => c.Write(segment.Text));
    }

    private readonly IConsole mConsole;
    private readonly IColoredActionFactory mFactory;
    private readonly StyleSheetMap mMap;
    private readonly IParser mParser;
    private readonly StyleSheet mStyleSheet;
  }
}