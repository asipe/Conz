namespace Conz.Core {
  public class Conzole {
    public Conzole(ConzoleConfig config) {
      mColoredAction = new ColoredAction(config.Console, config.ForegroundColor, config.BackgroundColor);
    }

    public void WriteLine(string value) {
      mColoredAction.Execute(c => c.WriteLine(value));
    }

    private readonly ColoredAction mColoredAction;
  }
}