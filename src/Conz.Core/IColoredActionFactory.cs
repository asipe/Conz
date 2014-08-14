using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public interface IColoredActionFactory {
    IColoredAction Build(IConsole console, Class defaultClass, Class currentClass);
  }
}