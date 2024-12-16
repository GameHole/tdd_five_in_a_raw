using System;

namespace Five
{
    public class MatchServce
    {
        public Domain domain { get; }

        public MatchServce(Domain domain)
        {
            this.domain = domain;
        }
        private Room FindNotStartGame()
        {
            foreach (var item in domain.roomRsp)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return domain.roomRsp.NewRoom();
        }

        public void Stop()
        {
            domain.Stop();
        }

        public Result Match(int id)
        {
            var player = domain.playerRsp.FindPlayer(id);
            lock (domain.roomRsp.lcoker)
            {
                var room = domain.roomRsp.GetRoom(player.RoomId);
                if (room == null)
                {
                    room = FindNotStartGame();
                    room.Join(player);
                    if (room.isFull())
                    {
                        room.Start();
                    }
                    return ResultDefine.Success;
                }
                if (room.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                return ResultDefine.Matching;
            }
        }

        public Result Cancel(int id)
        {
            var player = domain.playerRsp.FindPlayer(id);
            lock (domain.roomRsp.lcoker)
            {
                var room = domain.roomRsp.GetRoom(player.RoomId);
                if (room == null)
                {
                    return ResultDefine.NotInMatching;
                }
                if (room.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                room.Remove(player);
                return ResultDefine.Success;
            }
        }
    }
}
