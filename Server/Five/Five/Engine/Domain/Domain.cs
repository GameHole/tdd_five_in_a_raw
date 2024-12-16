using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Domain
    {
        public LoginServce loginServce { get; private set; }
        public PlayerRepository playerRsp { get; private set; }
        public RoomRepository roomRsp { get; private set; }
        public MatchServce matchServce { get; private set; }

        public Domain(IGameFactroy factroy, IdGenrator genrator)
        {
            roomRsp = new RoomRepository(factroy);
            playerRsp = new PlayerRepository();
            matchServce = new MatchServce(roomRsp, playerRsp);
            loginServce = new LoginServce(playerRsp, genrator);
        }
        public virtual void Stop()
        {
            playerRsp.Stop();
            roomRsp.Clear();
        }
    }
}
