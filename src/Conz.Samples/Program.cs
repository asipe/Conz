using System;
using System.Linq;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Samples {
  internal class Program {
    private static void Main() {
      try {
        DoWork();
      } catch (Exception e) {
        Console.WriteLine("e: {0}", e);
      }
    }

    private static void DoWork() {
      DoRainbow();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      DoFormatted();
    }

    private static void DoFormatted() {
      var conzole = new Conzole(new DotNetConsole(),
                                new Parser(),
                                new ColoredActionFactory(),
                                new StyleSheet(new Class("default", ConsoleColor.Black, ConsoleColor.White),
                                               new Class("error", ConsoleColor.Red, ConsoleColor.Black),
                                               new Class("warning", ConsoleColor.Yellow, ConsoleColor.Red)));
      conzole.WriteLine("This is default");
      conzole.WriteLine("|error|This is error|");
      conzole.WriteLine("|warning|This is warning|");
      conzole.WriteLine("");
      conzole.WriteLine("This is mixed formatted---   |warning|Attention:||error|An Error Occurred.|  Please do not panic!");
    }

    private static void DoRainbow() {
      var colors = Enum
        .GetValues(typeof(ConsoleColor))
        .Cast<ConsoleColor>()
        .ToArray();

      var reversedColors = colors
        .Reverse()
        .ToArray();

      var conzoles = colors
        .Select((color, x) => new Conzole(new DotNetConsole(),
                                          new Parser(),
                                          new ColoredActionFactory(),
                                          new StyleSheet(new Class(x.ToString(), reversedColors[x], color), null)))
        .ToArray();

      Array.ForEach(conzoles, con => con.WriteLine("Hello World"));
    }
  }
}