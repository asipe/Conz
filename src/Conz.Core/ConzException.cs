using System;
using System.Runtime.Serialization;

namespace Conz.Core {
  [Serializable]
  public class ConzException : Exception {
    public ConzException() {}
    public ConzException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    public ConzException(string msg) : base(msg) {}
    public ConzException(string msg, Exception e) : base(msg, e) {}
    public ConzException(string msg, params object[] fmt) : base(string.Format(msg, fmt)) {}
  }
}