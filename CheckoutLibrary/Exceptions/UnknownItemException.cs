using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLibrary.Exceptions
{
    public class UnknownItemException : Exception
    {
        public UnknownItemException(string message) : base(message) { }
    }
}
