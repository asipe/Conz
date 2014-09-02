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
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      MoreFormatting();
    }

    private static void MoreFormatting() {
      const string text = "This is some sample text.  |quote|\"This is a quote\".| and this isn't a quote.\r\n|sep|-------|\r\nNote: |note|Just a sample|\r\nYes this is just a sample.\r\n|sep|-------|";
      var conzole1 = new Conzole(new StyleSheet(new Class("default"),
                                               new Class("quote", ConzoleColor.Default, ConzoleColor.Yellow),
                                               new Class("note", ConzoleColor.Default, ConzoleColor.Red),
                                               new Class("sep", ConzoleColor.White, ConzoleColor.Black, 2)));
      var conzole2 = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Blue, ConzoleColor.Yellow),
                                               new Class("quote", ConzoleColor.Default, ConzoleColor.Cyan),
                                               new Class("note", ConzoleColor.White, ConzoleColor.Red),
                                               new Class("sep", ConzoleColor.Default, ConzoleColor.Red, 50)));
      conzole1.WriteLine(text);
      conzole2.WriteLine(text);
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