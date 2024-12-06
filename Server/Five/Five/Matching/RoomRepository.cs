using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class RoomRepository:IEnumerable<Room>
    {
        public int GameCount { get => games.Count; }
        public object lcoker => games;
        private ConcurrentDictionary<int, Room> games = new ConcurrentDictionary<int, Room>();
        private IGameFactroy factroy;

        public RoomRepository(IGameFactroy factroy)
        {
            this.factroy = factroy;
        }

      
        public void Clear()
        {
            foreach (var item in games.Values)
            {
                item.Stop();
            }
            games.Clear();
        }

        public Room NewRoom()
        {
            var game = factroy.Factroy();
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
