using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Matching
    {
        List<Game> games = new List<Game>();
        public int GameCount { get=> games.Count;}

        public void Match(Player player)
        {
            var game = FactroyGame();
            game.Join(player);
            player.Match();
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

        public void Cancel(Player player)
        {
            var game = player.game;
            game?.Remove(player);
            player.CancelMatch();
        }
    }
}
