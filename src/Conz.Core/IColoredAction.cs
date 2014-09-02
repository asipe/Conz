using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public interface IColoredAction {
    void Execute(params Action<IConsole>[] actions);
  }
}