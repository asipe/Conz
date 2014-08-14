using System.Collections.Generic;
using System.Linq;

namespace Conz.Core {
  public class StyleSheetMap {
    public StyleSheetMap(StyleSheet styleSheet) {
      mDefault = styleSheet.Default;
      mMap = styleSheet
        .Classes
        .ToDictionary(c => c.Name);
    }

    public Class this[string name] {
      get {
        Class result = null;
        if (name != null)
          mMap.TryGetValue(name, out result);
        return result ?? mDefault;
      }
    }

    private readonly Class mDefault;
    private readonly Dictionary<string, Class> mMap;
  }
}