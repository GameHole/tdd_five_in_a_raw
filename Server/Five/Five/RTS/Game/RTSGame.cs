using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSGame : AGame
    {
        public Dictionary<int,RTSCharater> charaters = new Dictionary<int, RTSCharater>();

        public float finishX => 20;

        public float finishY => 22.5f;

        public override void Start()
        {
            base.Start();
            foreach (var item in room.Players)
            {
                int id = item.Id;
                charaters.Add(id,SpwanChar(id));
            }
            var notify = new RTSStartNotify
            {
                info = charaters.Values,
                fshx = finishX,
                fshy = finishY
            };
            room.NotifyAllPlayer(notify);
        }
        private RTSCharater SpwanChar(int id)
        {
            var ch = new RTSCharater(id);
            ch.x = id * 10;
            ch.y = id + 1;
            return ch;
        }

        public override void Stop()
        {
            charaters.Clear();
        }
        public override Result Commit(Message message, Player player)
        {
            var ch = charaters[player.Id];
            MoveTo moveToMessage = message as MoveTo;
            ch.targetX = moveToMessage.x;
            ch.targetY = moveToMessage.y;
            return ResultDefine.Success;
        }
    }
}
