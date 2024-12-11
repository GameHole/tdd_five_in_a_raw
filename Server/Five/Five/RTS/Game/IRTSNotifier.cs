using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public interface IRTSNotifier
    {
        void StartRTS(List<Charater> info, float fshx, float fshy);
    }
}
