using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Matching
    {
        List<Game> games = new List<Game>();
        public int GameCount { get=> games.Count;}

        public void Match(Matcher master)
        {
            var game = FactroyGame();
            game.Join(master.Player);
            master.Matched();
            if (game.isFull())
            {
                game.Start();
            }
        }

        private Game FactroyGame()
        {
            foreach (var item in games)
            {
                if (!item.isFull())
                {
                    return item;
                }
            }
            return NewGame();
        }

        private Game NewGame()
        {
            var game = new Game();
            game.Id = games.Count + 1;
            games.Add(game);
            return game;
        }

        public Game GetGame(int id)
        {
            return games.Find(g => g.Id == id);
        }

        public void Cancel(Matcher master)
        {
            var game = GetGame(master.GameId);
            game?.Remove(master.Player);
            master.Canceled();
        }
    }
}
