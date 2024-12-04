using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class GameMgr:IEnumerable<Room>
    {
        ConcurrentDictionary<int, Room> games = new ConcurrentDictionary<int, Room>();
        public int GameCount { get=> games.Count;}
        public object lcoker => games;


        public void Clear()
        {
            foreach (var item in games.Values)
            {
                item.Stop();
            }
            games.Clear();
        }

        public Room NewGame()
        {
            var game = new Game();
            game.Id = games.Count + 1;
            games.TryAdd(game.Id,game);
            return game;
        }

        public Room GetRoom(int id)
        {
            games.TryGetValue(id, out var game);
            return game;
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return games.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
