using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{

    [Serializable]
    public class StreamException : Exception
    {
        public StreamException(string message) : base(message)
        {
        }
    }
}
