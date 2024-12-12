using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSGame : AGame
    {
        public Dictionary<int,Charater> charaters = new Dictionary<int, Charater>();

        public float finishX => 20;

        public float finishY => 22.5f;

        public override void Start()
        {
            base.Start();
            foreach (var item in room.Players)
            {
                int id = item.PlayerId;
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
        private Charater SpwanChar(int id)
        {
            var ch = new Charater { id = id };
            ch.x = id * 10;
            ch.y = id + 1;
            return ch;
        }

        public override void Stop()
        {
            charaters.Clear();
        }

        public void MoveTo(float x, float y, Player player)
        {
            var ch = charaters[player.PlayerId];
            ch.targetX = x;
            ch.targetY = y;
        }
        public override Result Commit(Message message, Player player)
        {
            var ch = charaters[player.PlayerId];
            MoveTo moveToMessage = message as MoveTo;
            ch.targetX = moveToMessage.x;
            ch.targetY = moveToMessage.y;
            return ResultDefine.Success;
        }
    }
}
