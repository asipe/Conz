using System;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class ConzoleConfig {
    public IConsole Console{get;set;}
    public ConsoleColor ForegroundColor{get;set;}
    public ConsoleColor BackgroundColor{get;set;}
  }
}