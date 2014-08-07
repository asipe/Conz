using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole {
    public Conzole(IConsole console, ConzoleConfig config) {
      mConsole = console;
      mConfig = config;
    }

    public void WriteLine(string value) {
      var original = mConsole.ForegroundColor;
      mConsole.ForegroundColor = mConfig.ForegroundColor;
      mConsole.WriteLine(value);
      mConsole.ForegroundColor = original;
    }

    private readonly IConsole mConsole;
    private readonly ConzoleConfig mConfig;
  }
}