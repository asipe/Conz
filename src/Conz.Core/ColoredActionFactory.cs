using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredActionFactory : IColoredActionFactory {
    public IColoredAction Build(IConsole console, Class defaultClass, Class currentClass) {
      return new ColoredAction(console, defaultClass, currentClass);
    }
  }
}