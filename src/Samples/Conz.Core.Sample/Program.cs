using System;
using System.Linq;

namespace Conz.Core.Sample {
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
      var conzole = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Black, ConzoleColor.White),
                                               new Class("error", ConzoleColor.Red, ConzoleColor.Black),
                                               new Class("warning", ConzoleColor.Yellow, ConzoleColor.Red)));
      conzole.WriteLine("This is default");
      conzole.WriteLine("|error|This is error|");
      conzole.WriteLine("|warning|This is warning|");
      conzole.WriteLine("");
      conzole.WriteLine("This is mixed formatted---   |warning|Attention:||error|An Error Occurred.|  Please do not panic!");
    }

    private static void DoRainbow() {
      var colors = Enum
        .GetValues(typeof(ConzoleColor))
        .Cast<ConzoleColor>()
        .Where(cc => cc != ConzoleColor.Default)
        .ToArray();

      var reversedColors = colors
        .Reverse()
        .ToArray();

      var conzoles = colors
        .Select((color, x) => new Conzole(new StyleSheet(new Class(x.ToString(), reversedColors[x], color), null)))
        .ToArray();

      Array.ForEach(conzoles, con => con.WriteLine("Hello World"));
    }
  }
}