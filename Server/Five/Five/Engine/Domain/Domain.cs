using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Domain
    {
        public PlayerRepository playerRsp { get; private set; }
        public RoomRepository roomRsp { get; private set; }
        public MatchServce matchServce { get; private set; }

        public Domain(IGameFactroy factroy, IdGenrator genrator)
        {
            roomRsp = new RoomRepository(factroy);
            playerRsp = new PlayerRepository(genrator);
            matchServce = new MatchServce(roomRsp, playerRsp);
        }
        public virtual void Stop()
        {
            playerRsp.Stop();
            roomRsp.Clear();
        }

        public virtual void Login(AClient socket)
        {
            playerRsp.Login(socket);
        }
    }
}
