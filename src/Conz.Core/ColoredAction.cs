using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ColoredAction : IColoredAction {
    public ColoredAction(IConsole console, Style defaultStyle, Style currentStyle) {
      mConsole = console;
      mDefaultstyle = defaultStyle ?? _EmptyStyle;
      mCurrentStyle = currentStyle ?? _EmptyStyle;
    }

    public void Execute(Action<IConsole> action) {
      var originalForegroundColor = mConsole.ForegroundColor;
      var originalBackgroundColor = mConsole.BackgroundColor;
      mConsole.ForegroundColor = GetCurrentColor(mCurrentStyle.Color, mDefaultstyle.Color, originalForegroundColor);
      mConsole.BackgroundColor = GetCurrentColor(mCurrentStyle.BackgroundColor, mDefaultstyle.BackgroundColor, originalBackgroundColor);
      try {
        action.Invoke(mConsole);
      } finally {
        mConsole.ForegroundColor = originalForegroundColor;
        mConsole.BackgroundColor = originalBackgroundColor;
      }
    }

    private static ConsoleColor GetCurrentColor(ConsoleColor? currentStyleColor,
                                                ConsoleColor? defaultStyleColor,
                                                ConsoleColor currentConsoleColor) {
      return currentStyleColor.HasValue
               ? currentStyleColor.Value
               : defaultStyleColor.HasValue
                   ? defaultStyleColor.Value
                   : currentConsoleColor;
    }

    private static readonly Style _EmptyStyle = new Style("", null, null);
    private readonly IConsole mConsole;
    private readonly Style mCurrentStyle;
    private readonly Style mDefaultstyle;
  }
}