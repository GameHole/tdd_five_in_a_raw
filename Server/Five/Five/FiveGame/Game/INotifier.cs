using Five.RTS;
using System.Collections.Generic;

namespace Five
{
    public interface INotifier
    {
        void Send(Message notify);
    }
}