using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    internal class RTSGameNotifier
    {
        private IRoom game;
        public RTSGameNotifier(IRoom game)
        {
            this.game = game;
        }
        public void NotifyRTSStart(List<Charater> charaters, float finishX, float finishY)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Send(new RTSStartNotify { info = charaters, fshx = finishX, fshy = finishY });
            }
        }
    }
}
