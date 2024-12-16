using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class LoginServce
    {
        private PlayerRepository playerRsp;
        IdGenrator gen;
        public LoginServce(PlayerRepository playerRsp, IdGenrator gen)
        {
            this.playerRsp = playerRsp;
            this.gen = gen;
        }

        public int Login(INotifier notifier)
        {
            var id = gen.Genrate();
            var player = new Player(id);
            player.notifier = notifier;
            playerRsp.Add(player);
            return id;
        }

        public void OutLine(int playerId)
        {
            playerRsp.FindPlayer(playerId).OutLine();
            playerRsp.Remove(playerId);
        }
    }
}
