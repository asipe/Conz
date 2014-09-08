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
      Basic();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();

      MoreFormatting();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();

      Rainbow();
    }

    private static void Basic() {
      var conz = new Conzole(new StyleSheet(new Class("default")));
      conz.WriteLine("Default style sheet which uses the current Console default values");

      conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow)));
      conz.WriteLine("Default style sheet which has a yellow background");

      conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black)));
      conz.WriteLine("Default style sheet which has a yellow background and a black foreground");

      conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black, 5)));
      conz.WriteLine("Default style sheet which has a yellow background and a black foreground and an indent of 5 spaces");

      conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black),
                                           new Class("error", ConzoleColor.Red)));
      conz.WriteLine("|error|error style| defined which has a red background");

      conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black),
                                           new Class("error", ConzoleColor.Red),
                                           new Class("notice", ConzoleColor.Default, ConzoleColor.DarkCyan)));
      conz.WriteLine("|notice|notice style| defined which has a cyan foreground");

      conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black),
                                        new Class("error", ConzoleColor.Red)), 
                                        '^');
      conz.WriteLine("^error^error style^ conzole defined using a custom format definition character");

      conz = new Conzole(BuiltInStyles.ForegroundColorOnly._Instance);
      conz.WriteLine("Some |y|default| |g|styling|");
    }

    private static void MoreFormatting() {
      const string text = "This is some sample text.  |quote|\"This is a quote\".| and this isn't a quote.\r\n|sep|-------\r\n|Note: |note|Just a sample\r\n|Yes this is just a sample.\r\n|sep|-------|";
      var conzole1 = new Conzole(new StyleSheet(new Class("default"),
                                                new Class("quote", ConzoleColor.Default, ConzoleColor.Yellow),
                                                new Class("note", ConzoleColor.Default, ConzoleColor.Red),
                                                new Class("sep", ConzoleColor.White, ConzoleColor.Black, 2)));
      var conzole2 = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Blue, ConzoleColor.Yellow),
                                                new Class("quote", ConzoleColor.Default, ConzoleColor.Cyan),
                                                new Class("note", ConzoleColor.White, ConzoleColor.Red),
                                                new Class("sep", ConzoleColor.Default, ConzoleColor.Red, 50)));
      var conzole3 = new Conzole(new StyleSheet(new Class("default", 10)));
      conzole1.WriteLine(text);
      conzole2.WriteLine(text);
      conzole3.WriteLine(text);
    }

    private static void Rainbow() {
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
      Array.ForEach(conzoles.Reverse().ToArray(), con => con.WriteLine("Hello World"));
    }
  }
}