using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public interface IColoredAction {
    void Execute(Action<IConsole> action);
  }
}