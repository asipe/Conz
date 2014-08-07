using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredAction {
    public ColoredAction(IConsole console, ConsoleColor foregroundColor) {
      mConsole = console;
      mForegroundColor = foregroundColor;
    }

    public void Execute(Action<IConsole> action) {
      var originalForegroundColor = mConsole.ForegroundColor;
      mConsole.ForegroundColor = mForegroundColor;
      try {
        action.Invoke(mConsole);
      } finally {
        mConsole.ForegroundColor = originalForegroundColor;
      }
    }

    private readonly IConsole mConsole;
    private readonly ConsoleColor mForegroundColor;
  }
}