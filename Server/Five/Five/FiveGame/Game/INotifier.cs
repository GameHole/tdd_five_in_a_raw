using Five.RTS;
using System.Collections.Generic;

namespace Five
{
    public interface INotifier
    {
        void Played(int x, int y, int id);
        void Start(PlayerInfo[] info);
        void Finish(int id);
        void Turn(int id);
    }
}