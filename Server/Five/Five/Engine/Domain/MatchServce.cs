using System;
using System.Collections.Generic;

namespace Five
{
    public class MatchServce
    {
        private RoomRepository roomRsp;
        private PlayerRepository playerRsp;

        public MatchServce(RoomRepository roomRsp,PlayerRepository playerRsp)
        {
            this.roomRsp = roomRsp;
            this.playerRsp = playerRsp;
        }
        private Room FindNotStartGame()
        {
            foreach (var item in roomRsp)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return roomRsp.NewRoom();
        }

        public Result Match(int id)
        {
            var player = playerRsp.FindPlayer(id);
            lock (roomRsp.lcoker)
            {
                var room = roomRsp.GetRoom(player.RoomId);
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
            var player = playerRsp.FindPlayer(id);
            lock (roomRsp.lcoker)
            {
                var room = roomRsp.GetRoom(player.RoomId);
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
