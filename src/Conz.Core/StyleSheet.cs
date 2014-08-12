namespace Conz.Core {
  public class StyleSheet {
    public StyleSheet(Class @default, Class[] classes) {
      Default = @default;
      Classes = classes;
    }

    public Class Default{get;private set;}
    public Class[] Classes{get;private set;}
  }
}