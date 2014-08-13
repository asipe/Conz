using System;

namespace Conz.Core.ConsoleAbstraction {
  public class DotNetConsole : IConsole {
    public ConsoleColor BackgroundColor {
      get {return Console.BackgroundColor;}
      set {Console.BackgroundColor = value;}
    }

    public ConsoleColor ForegroundColor {
      get {return Console.ForegroundColor;}
      set {Console.ForegroundColor = value;}
    }

    public void WriteLine() {
      Console.WriteLine();
    }

    public void WriteLine(string value) {
      Console.WriteLine(value);
    }

    public void Write(string value) {
      Console.Write(value);
    }
  }
}