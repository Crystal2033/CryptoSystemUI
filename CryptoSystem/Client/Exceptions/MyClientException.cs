using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Exceptions
{
    public sealed class MyClientException : Exception
    {
        public MyClientException(string msg) : base(msg) { }
    }
}
