using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredAction : IColoredAction {
    public ColoredAction(IConsole console, Class defaultClass, Class currentClass) {
      mConsole = console;
      mDefaultClass = defaultClass ?? Constants._EmptyClass;
      mCurrentClass = currentClass ?? Constants._EmptyClass;
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

    private static ConsoleColor GetCurrentColor(ConzoleColor? currentClassColor,
                                                ConzoleColor? defaultClassColor,
                                                ConsoleColor currentConsoleColor) {
      return currentClassColor.HasValue
               ? (ConsoleColor)currentClassColor.Value
               : defaultClassColor.HasValue
                   ? (ConsoleColor)defaultClassColor.Value
                   : currentConsoleColor;
    }

    private readonly IConsole mConsole;
    private readonly Class mCurrentClass;
    private readonly Class mDefaultClass;
  }
}