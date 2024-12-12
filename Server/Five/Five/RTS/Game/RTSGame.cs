using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSGame : AGame
    {
        public List<Charater> charaters = new List<Charater>();

        public float finishX => 20;

        public float finishY => 22.5f;

        public override void Start()
        {
            base.Start();
            int i = 0;
            foreach (var item in room.Players)
            {
                var id = i++;
                Charater ch = SpwanChar(id);
                item.PlayerId = id;
                charaters.Add(ch);
            }
            var notify = new RTSStartNotify
            {
                info = charaters,
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
            MoveToMessage moveToMessage = message as MoveToMessage;
            ch.targetX = moveToMessage.x;
            ch.targetY = moveToMessage.y;
            return ResultDefine.Success;
        }
    }
}
