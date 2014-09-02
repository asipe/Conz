using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredAction : IColoredAction {
    public ColoredAction(IConsole console, Class defaultClass, Class currentClass) {
      mConsole = console;
      mDefaultClass = defaultClass ?? Constants._EmptyClass;
      mCurrentClass = currentClass ?? Constants._EmptyClass;
    }

    public void Execute(params Action<IConsole>[] actions) {
      var originalForegroundColor = mConsole.ForegroundColor;
      var originalBackgroundColor = mConsole.BackgroundColor;
      mConsole.ForegroundColor = GetCurrentColor(mCurrentClass.Color, mDefaultClass.Color, originalForegroundColor);
      mConsole.BackgroundColor = GetCurrentColor(mCurrentClass.BackgroundColor, mDefaultClass.BackgroundColor, originalBackgroundColor);
      try {
        Array.ForEach(actions, action => action.Invoke(mConsole));
      } finally {
        mConsole.ForegroundColor = originalForegroundColor;
        mConsole.BackgroundColor = originalBackgroundColor;
      }
    }

    private static ConsoleColor GetCurrentColor(ConzoleColor currentClassColor,
                                                ConzoleColor defaultClassColor,
                                                ConsoleColor currentConsoleColor) {
      return (currentClassColor != ConzoleColor.Default)
               ? (ConsoleColor)currentClassColor
               : (defaultClassColor != ConzoleColor.Default)
                   ? (ConsoleColor)defaultClassColor
                   : currentConsoleColor;
    }

    private readonly IConsole mConsole;
    private readonly Class mCurrentClass;
    private readonly Class mDefaultClass;
  }
}