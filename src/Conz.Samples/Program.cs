﻿using System;
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

      var colors = Enum
        .GetValues(typeof(ConsoleColor))
        .Cast<ConsoleColor>()
        .ToArray();

      var reversedColors = colors
        .Reverse()
        .ToArray();

      var conzoles = colors
        .Select((color, x) => new Conzole(new ConzoleConfig {
                                                              Console = console,
                                                              ForegroundColor = color,
                                                              BackgroundColor = reversedColors[x]
                                                            }))
        .ToArray();

      Array.ForEach(conzoles, con => con.WriteLine("Hello World"));
    }
  }
}