using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class Matching
    {
        ConcurrentDictionary<int,Game> games = new ConcurrentDictionary<int, Game>();
        public int GameCount { get=> games.Count;}

        public void Match(Matcher master)
        {
            lock (games)
            {
                var game = FindNotStartGame();
                game.Join(master.Player);
                master.Matched();
                if (game.isFull())
                {
                    game.Start();
                }
            }
        }

        private Game FindNotStartGame()
        {
            foreach (var item in games.Values)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return NewGame();
        }

        public void Clear()
        {
            foreach (var item in games.Values)
            {
                item.Stop();
            }
            games.Clear();
        }

        private Game NewGame()
        {
            var game = new Game();
            game.Id = games.Count + 1;
            games.TryAdd(game.Id,game);
            return game;
        }

        public Game GetGame(int id)
        {
            games.TryGetValue(id, out var game);
            return game;
        }

        public Result Cancel(Matcher master)
        {
            lock (games)
            {
                var game = GetGame(master.GameId);
                if (game == null)
                {
                    return ResultDefine.NotInMatching;
                }
                if (game.IsRunning)
                {
                    return ResultDefine.GameStarted;
                }
                game.Remove(master.Player);
                master.Canceled();
                return ResultDefine.Success;
            }
           
        }
    }
}
