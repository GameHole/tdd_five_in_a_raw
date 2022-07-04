using System;

namespace Five
{
    class GameNotifier
    {
        private Game game;
        public GameNotifier(Game game)
        {
            this.game = game;
        }
        public void NotifyPlay(int x, int y, int playerId)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Played(x, y, playerId);
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
                item.notifier.Start(infos);
            }
        }
        public void NotifyFinish(int id)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Finish(id);
            }
        }

        public void NotifyTurn(int index)
        {
            foreach (var item in game.Players)
            {
                item.notifier.Turn(index);
            }
        }
    }
}