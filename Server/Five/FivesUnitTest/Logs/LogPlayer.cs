using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class LogPlayer : Player
    {
        public string log;
        internal object msg;

        public LogPlayer(int id) : base(id) { }
        public override Result Commit(Message message)
        {
            msg = message;
            return base.Commit(message);
        }
        public override void Reset()
        {
            base.Reset();
            log += $"Reset ";
        }
        public override void OutLine()
        {
            base.OutLine();
            log += "OutLine ";
        }

        public static LogPlayer EmntyLog(int id=0)
        {
            var player = new LogPlayer(id);
            player.log = "";
            return player;
        }

       
    }
}
