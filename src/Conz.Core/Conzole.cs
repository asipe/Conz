using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole {
    public Conzole(IConsole console) {
      mConsole = console;
    }

    public void WriteLine(string value) {
      mConsole.WriteLine(value); 
    }

    private readonly IConsole mConsole;
  }
}