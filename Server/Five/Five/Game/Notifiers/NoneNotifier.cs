using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class NoneNotifier : INotifier
    {
        public void Finish(int id) { }

        public void Played(int x, int y, int id) { }

        public void Start(PlayerInfo[] info) { }

        public void Turn(int id) { }
    }
}
