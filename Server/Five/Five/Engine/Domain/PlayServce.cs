using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayServce
    {
        private PlayerRepository playerRsp;
        private RoomRepository roomRsp;

        public PlayServce(PlayerRepository playerRsp, RoomRepository roomRsp)
        {
            this.playerRsp = playerRsp;
            this.roomRsp = roomRsp;
        }

        public Result Commit(int id, Message message)
        {
            var player = playerRsp.FindPlayer(id);
            var room = roomRsp.GetRoom(player.RoomId);
            if (room == null)
                return ResultDefine.PlayerNotInTheGame;
            if (!room.IsRunning)
                return ResultDefine.GameNotStart;
            var result = room.game.Commit(message, player);
            return result;
        }
    }
}
