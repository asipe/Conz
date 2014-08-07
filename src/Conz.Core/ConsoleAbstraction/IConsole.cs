using System;

namespace Conz.Core.ConsoleAbstraction {
  public interface IConsole {
    ConsoleColor BackgroundColor{get;set;}
    ConsoleColor ForegroundColor{get;set;}
    void WriteLine(string value);
  }
}