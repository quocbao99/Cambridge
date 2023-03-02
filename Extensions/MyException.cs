using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Extensions
{
    public class MyException : Exception
    {
        public int HttpStatusCode { get; set; }
        public MyException() : base() { }

        public MyException(string message , int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public MyException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        public MyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
