using Five;
using Five.RTS;
using System.Collections.Generic;

namespace FivesUnitTest
{
    internal class LogNotifier : INotifier
    {
        internal string log;

        public void Finish(PlayerIdNotify notify)
        {
            log += $" Finish:{notify.playerId}";
        }

        public void Played(PlayedNotify notify)
        {
            log += $" Played({notify.x},{notify.y})id:{notify.id}";
        }

        public void Start(StartNotify notify)
        {
            log += "Start";
            foreach (var item in notify.infos)
            {
                log += $"({item.PlayerId},{item.Chess})";
            }
        }

   

        public void Turn(PlayerIdNotify notify)
        {
            log += $" Turn id:{notify.playerId}";
        }
    }
}