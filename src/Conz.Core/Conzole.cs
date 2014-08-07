using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole {
    public Conzole(IConsole console, ConzoleConfig config) {
      mColoredAction = new ColoredAction(console, config.ForegroundColor);
    }

    public void WriteLine(string value) {
      mColoredAction.Execute(c => c.WriteLine(value));
    }

    private readonly ColoredAction mColoredAction;
  }
}