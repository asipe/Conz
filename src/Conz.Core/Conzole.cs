using System;
using System.IO;
using Conz.Core.ConsoleAbstraction;

namespace Conz.Core {
  public class Conzole : IConsole {
    public Conzole(StyleSheet styleSheet) : this(new DotNetConsole(), new Parser(), new ColoredActionFactory(), styleSheet) {}

    public Conzole(IConsole console,
                   IParser parser,
                   IColoredActionFactory factory,
                   StyleSheet styleSheet) {
      mMap = new StyleSheetMap(styleSheet);
      mConsole = console;
      mParser = parser;
      mFactory = factory;
      mStyleSheet = styleSheet;
    }

    public TextWriter Error {
      get {return mConsole.Error;}
    }

    public TextReader In {
      get {return mConsole.In;}
    }

    public TextWriter Out {
      get {return mConsole.Out;}
    }

    public ConsoleColor BackgroundColor {
      get {return mConsole.BackgroundColor;}
      set {mConsole.BackgroundColor = value;}
    }

    public ConsoleColor ForegroundColor {
      get {return mConsole.ForegroundColor;}
      set {mConsole.ForegroundColor = value;}
    }

    public void WriteLine() {
      mConsole.WriteLine();
    }

    public void WriteLine(bool value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(char value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(char[] buffer) {
      DoWorkWithDefault(c => c.WriteLine(buffer));
    }

    public void WriteLine(decimal value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(double value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(int value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(long value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(object value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(float value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(string value) {
      Write(value);
      mConsole.WriteLine();
    }

    public void WriteLine(uint value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(ulong value) {
      DoWorkWithDefault(c => c.WriteLine(value));
    }

    public void WriteLine(string format, object arg0) {
      Write(format, arg0);
      WriteLine();
    }

    public void WriteLine(string format, params object[] arg) {
      Write(format, arg);
      WriteLine();
    }

    public void WriteLine(char[] buffer, int index, int count) {
      DoWorkWithDefault(c => c.WriteLine(buffer, index, count));
    }

    public void WriteLine(string format, object arg0, object arg1) {
      Write(format, arg0, arg1);
      WriteLine();
    }

    public void WriteLine(string format, object arg0, object arg1, object arg2) {
      Write(format, arg0, arg1, arg2);
      WriteLine();
    }

    public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3) {
      Write(format, arg0, arg1, arg2, arg3);
      WriteLine();
    }

    public void Write(bool value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(char value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(char[] buffer) {
      DoWorkWithDefault(c => c.Write(buffer));
    }

    public void Write(decimal value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(double value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(int value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(long value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(object value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(float value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(string value) {
      Array.ForEach(mParser.Parse(value), segment => DoWork(mMap[segment.Class], c => c.Write(segment.Text)));
    }

    public void Write(uint value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(ulong value) {
      DoWorkWithDefault(c => c.Write(value));
    }

    public void Write(string format, object arg0) {
      Write(string.Format(format, arg0));
    }

    public void Write(string format, params object[] arg) {
      Write(string.Format(format, arg));
    }

    public void Write(char[] buffer, int index, int count) {
      DoWorkWithDefault(c => c.Write(buffer, index, count));
    }

    public void Write(string format, object arg0, object arg1) {
      Write(string.Format(format, arg0, arg1));
    }

    public void Write(string format, object arg0, object arg1, object arg2) {
      Write(string.Format(format, arg0, arg1, arg2));
    }

    public void Write(string format, object arg0, object arg1, object arg2, object arg3) {
      Write(string.Format(format, arg0, arg1, arg2, arg3));
    }

    public string ReadLine() {
      return mConsole.ReadLine();
    }

    public ConsoleKeyInfo ReadKey() {
      return mConsole.ReadKey();
    }

    public ConsoleKeyInfo ReadKey(bool intercept) {
      return mConsole.ReadKey(intercept);
    }

    public void ResetColor() {
      mConsole.ResetColor();
    }

    private void DoWork(Class @class, Action<IConsole> action) {
      mFactory
        .Build(mConsole, mStyleSheet.Default, @class)
        .Execute(action);
    }

    private void DoWorkWithDefault(Action<IConsole> action) {
      DoWork(mStyleSheet.Default, action);
    }

    private readonly IConsole mConsole;
    private readonly IColoredActionFactory mFactory;
    private readonly StyleSheetMap mMap;
    private readonly IParser mParser;
    private readonly StyleSheet mStyleSheet;
  }
}