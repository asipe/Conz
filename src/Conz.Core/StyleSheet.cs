namespace Conz.Core {
  public class StyleSheet {
    public StyleSheet(Style @default, Style[] styles) {
      Default = @default;
      Styles = styles;
    }

    public Style Default{get;private set;}
    public Style[] Styles{get;private set;}
  }
}