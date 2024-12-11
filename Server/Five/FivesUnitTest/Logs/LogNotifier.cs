using Five;
using Five.RTS;
using System.Collections.Generic;

namespace FivesUnitTest
{
    internal class LogNotifier : INotifier
    {
        internal string log;

        public void Finish(int id)
        {
            log += $" Finish:{id}";
        }

        public void Played(int x, int y, int id)
        {
            log += $" Played({x},{y})id:{id}";
        }

        public void Start(PlayerInfo[] info)
        {
            log += "Start";
            foreach (var item in info)
            {
                log += $"({item.PlayerId},{item.Chess})";
            }
        }

   

        public void Turn(int id)
        {
            log += $" Turn id:{id}";
        }
    }
}