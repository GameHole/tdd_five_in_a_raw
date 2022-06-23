using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public abstract class ASocket
    {
        public abstract void Send(Message message);
    }
}
