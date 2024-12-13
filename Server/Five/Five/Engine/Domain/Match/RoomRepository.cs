using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class RoomRepository:IEnumerable<Room>
    {
        public int Count { get => rooms.Count; }
        public object lcoker => rooms;
        private ConcurrentDictionary<int, Room> rooms = new ConcurrentDictionary<int, Room>();
        private IGameFactroy factroy;

        public RoomRepository(IGameFactroy factroy)
        {
            this.factroy = factroy;
        }
      
        public void Clear()
        {
            foreach (var item in rooms.Values)
            {
                item.Stop();
            }
            rooms.Clear();
        }

        public Room NewRoom()
        {
            var game = factroy.Factroy();
            var room = new Room(game);
            game.Init(room);
            room.Id = rooms.Count + 1;
            rooms.TryAdd(room.Id,room);
            return room;
        }

        public Room GetRoom(int id)
        {
            rooms.TryGetValue(id, out var game);
            return game;
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return rooms.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
