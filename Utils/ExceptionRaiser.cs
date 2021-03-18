using System;
using System.Globalization;

namespace backend_shop.Utils
{
    public class ExceptionRaiser : Exception
    {
        public ExceptionRaiser() : base() { }

        public ExceptionRaiser(string message) : base (message) {  }
        public ExceptionRaiser(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
