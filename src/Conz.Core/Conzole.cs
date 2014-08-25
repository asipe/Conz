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
      throw new NotImplementedException();
    }

    public void WriteLine(bool value) {
      throw new NotImplementedException();
    }

    public void WriteLine(char value) {
      throw new NotImplementedException();
    }

    public void WriteLine(char[] buffer) {
      throw new NotImplementedException();
    }

    public void WriteLine(decimal value) {
      throw new NotImplementedException();
    }

    public void WriteLine(double value) {
      throw new NotImplementedException();
    }

    public void WriteLine(int value) {
      throw new NotImplementedException();
    }

    public void WriteLine(long value) {
      throw new NotImplementedException();
    }

    public void WriteLine(object value) {
      throw new NotImplementedException();
    }

    public void WriteLine(float value) {
      throw new NotImplementedException();
    }

    public void WriteLine(string value) {
      Write(value);
      mConsole.WriteLine();
    }

    public void WriteLine(uint value) {
      throw new NotImplementedException();
    }

    public void WriteLine(ulong value) {
      throw new NotImplementedException();
    }

    public void WriteLine(string format, object arg0) {
      throw new NotImplementedException();
    }

    public void WriteLine(string format, params object[] arg) {
      throw new NotImplementedException();
    }

    public void WriteLine(char[] buffer, int index, int count) {
      throw new NotImplementedException();
    }

    public void WriteLine(string format, object arg0, object arg1) {
      throw new NotImplementedException();
    }

    public void WriteLine(string format, object arg0, object arg1, object arg2) {
      throw new NotImplementedException();
    }

    public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3) {
      throw new NotImplementedException();
    }

    public void Write(bool value) {
      throw new NotImplementedException();
    }

    public void Write(char value) {
      throw new NotImplementedException();
    }

    public void Write(char[] buffer) {
      throw new NotImplementedException();
    }

    public void Write(decimal value) {
      throw new NotImplementedException();
    }

    public void Write(double value) {
      throw new NotImplementedException();
    }

    public void Write(int value) {
      throw new NotImplementedException();
    }

    public void Write(long value) {
      throw new NotImplementedException();
    }

    public void Write(object value) {
      throw new NotImplementedException();
    }

    public void Write(float value) {
      throw new NotImplementedException();
    }

    public void Write(string value) {
      Array.ForEach(mParser.Parse(value), segment => DoWork(mMap[segment.Class], c => c.Write(segment.Text)));
    }

    public void Write(uint value) {
      throw new NotImplementedException();
    }

    public void Write(ulong value) {
      throw new NotImplementedException();
    }

    public void Write(string format, object arg0) {
      throw new NotImplementedException();
    }

    public void Write(string format, params object[] arg) {
      throw new NotImplementedException();
    }

    public void Write(char[] buffer, int index, int count) {
      throw new NotImplementedException();
    }

    public void Write(string format, object arg0, object arg1) {
      throw new NotImplementedException();
    }

    public void Write(string format, object arg0, object arg1, object arg2) {
      throw new NotImplementedException();
    }

    public void Write(string format, object arg0, object arg1, object arg2, object arg3) {
      throw new NotImplementedException();
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

    private readonly IConsole mConsole;
    private readonly IColoredActionFactory mFactory;
    private readonly StyleSheetMap mMap;
    private readonly IParser mParser;
    private readonly StyleSheet mStyleSheet;
  }
}