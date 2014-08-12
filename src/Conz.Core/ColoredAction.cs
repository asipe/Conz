using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredAction : IColoredAction {
    public ColoredAction(IConsole console, Class defaultClass, Class currentClass) {
      mConsole = console;
      mDefaultClass = defaultClass ?? _EmptyClass;
      mCurrentClass = currentClass ?? _EmptyClass;
    }

    public void Execute(Action<IConsole> action) {
      var originalForegroundColor = mConsole.ForegroundColor;
      var originalBackgroundColor = mConsole.BackgroundColor;
      mConsole.ForegroundColor = GetCurrentColor(mCurrentClass.Color, mDefaultClass.Color, originalForegroundColor);
      mConsole.BackgroundColor = GetCurrentColor(mCurrentClass.BackgroundColor, mDefaultClass.BackgroundColor, originalBackgroundColor);
      try {
        action.Invoke(mConsole);
      } finally {
        mConsole.ForegroundColor = originalForegroundColor;
        mConsole.BackgroundColor = originalBackgroundColor;
      }
    }

    private static ConsoleColor GetCurrentColor(ConsoleColor? currentClassColor,
                                                ConsoleColor? defaultClassColor,
                                                ConsoleColor currentConsoleColor) {
      return currentClassColor.HasValue
               ? currentClassColor.Value
               : defaultClassColor.HasValue
                   ? defaultClassColor.Value
                   : currentConsoleColor;
    }

    private static readonly Class _EmptyClass = new Class("", null, null);
    private readonly IConsole mConsole;
    private readonly Class mCurrentClass;
    private readonly Class mDefaultClass;
  }
}