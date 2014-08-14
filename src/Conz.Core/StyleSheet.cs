namespace Conz.Core {
  public class StyleSheet {
    public StyleSheet(Class @default, params Class[] classes) {
      Default = @default ?? Constants._EmptyDefaultClass;
      Classes = classes ?? Constants._EmptyClasses;
    }

    public Class Default{get;private set;}
    public Class[] Classes{get;private set;}
  }
}