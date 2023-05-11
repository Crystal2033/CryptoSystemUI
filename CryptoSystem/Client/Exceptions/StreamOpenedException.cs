using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public sealed class StreamNotOpenedException : Exception
    {
        public StreamNotOpenedException(string msg) : base(msg) { }
    }
}
