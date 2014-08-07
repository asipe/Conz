using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredAction : IColoredAction {
    public ColoredAction(IConsole console, ConsoleColor foregroundColor, ConsoleColor backgroundColor) {
      mConsole = console;
      mForegroundColor = foregroundColor;
      mBackgroundColor = backgroundColor;
    }

    public void Execute(Action<IConsole> action) {
      var originalForegroundColor = mConsole.ForegroundColor;
      var originalBackgroundColor = mConsole.BackgroundColor;
      mConsole.ForegroundColor = mForegroundColor;
      mConsole.BackgroundColor = mBackgroundColor;
      try {
        action.Invoke(mConsole);
      } finally {
        mConsole.ForegroundColor = originalForegroundColor;
        mConsole.BackgroundColor = originalBackgroundColor;
      }
    }

    private readonly IConsole mConsole;
    private readonly ConsoleColor mForegroundColor;
    private readonly ConsoleColor mBackgroundColor;
  }
}