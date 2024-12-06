using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class RoomRepository:IEnumerable<Room>
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

        public Room NewRoom(AGame game)
        {
            var room = new Room(game);
            game.Init(room);
            room.Id = games.Count + 1;
            games.TryAdd(room.Id,room);
            return room;
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
