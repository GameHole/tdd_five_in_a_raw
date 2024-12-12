using Five.RTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class NoneNotifier : INotifier
    {
        public void Finish(PlayerIdNotify notify) { }

        public void Played(PlayedNotify notify) { }

        public void Start(StartNotify notify) { }

        public void Turn(PlayerIdNotify notify) { }
    }
}
