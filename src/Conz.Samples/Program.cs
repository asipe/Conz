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
      var console = new DotNetConsole();

      var conzoles = Enum
        .GetValues(typeof(ConsoleColor))
        .Cast<ConsoleColor>()
        .Select(color => new Conzole(console, new ConzoleConfig {ForegroundColor = color}))
        .ToArray();

      Array.ForEach(conzoles, con => con.WriteLine("Hello World"));
    }
  }
}