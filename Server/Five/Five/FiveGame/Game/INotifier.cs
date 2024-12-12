using Five.RTS;
using System.Collections.Generic;

namespace Five
{
    public interface INotifier
    {
        void Played(PlayedNotify notify);
        void Start(StartNotify notify);
        void Finish(PlayerIdNotify notify);
        void Turn(PlayerIdNotify notify);
    }
}