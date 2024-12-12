using Five.RTS;
using System;
using System.Collections.Generic;

namespace Five
{
    class GameNotifier
    {
        private IRoom game;
        public GameNotifier(IRoom game)
        {
            this.game = game;
        }
        public void NotifyPlay(int x, int y, int playerId)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Send(new PlayedNotify { x = x, y = y, id = playerId });
            }
        }
        public void NotifyStart()
        {
            var infos = new PlayerInfo[game.maxPlayer];
            int i = 0;
            foreach (var item in game.Players)
            {
                infos[i++] = new PlayerInfo(item.chess,item.PlayerId);
            }
            foreach (var item in game.Players)
            {
                item.notifier.Send(new StartNotify { playerId = item.PlayerId, infos = infos });
            }
        }
        public void NotifyFinish(int id)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Send(new PlayerIdNotify(MessageCode.FinishNotify, id));
            }
        }

        public void NotifyTurn(int index)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Send(new PlayerIdNotify(MessageCode.TurnNotify, index));
            }
        }
    }
}