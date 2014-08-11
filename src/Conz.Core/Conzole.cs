using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole {
    public Conzole(IConsole console, StyleSheet styleSheet) {
      mColoredAction = new ColoredAction(console, styleSheet.Default.Color.Value, styleSheet.Default.BackgroundColor.Value);
    }

    public void WriteLine(string value) {
      mColoredAction.Execute(c => c.WriteLine(value));
    }

    private readonly ColoredAction mColoredAction;
  }
}