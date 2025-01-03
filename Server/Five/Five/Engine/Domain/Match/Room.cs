﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Five
{
    public interface IRoom: IStopable
    {
        IEnumerable<Player> Players { get; }
        int maxPlayer { get; }
        void NotifyAllPlayer(Message notify)
        {
            foreach (var item in Players)
            {
                item.notifier.Send(notify);
            }
        }
    }
    public class Room: IRoom
    {
        private ConcurrentDictionary<int, Player> players;
        public IEnumerable<Player> Players { get => players.Values; }
        public int Id;
        private int _count = 0;
        public int maxPlayer { get; } = 2;
        public int PlayerCount { get => _count; }
        public bool IsRunning { get; private set; }
        public AGame game { get; set; }
        private IOutLineable startOut;
        public Room(AGame game)
        {
            this.game = game;
            players = new ConcurrentDictionary<int, Player>();
            startOut = new StartOutLineable(this);
        }

        public bool Join(Player player)
        {
            if (tryAdd())
            {
                player.RoomId = Id;
                players.TryAdd(player.Id, player);
                player.outlineable = new StopOutLineable(this, player);
                return true;
            }
            return false;
        }
        bool tryAdd()
        {
            var count = Interlocked.Increment(ref _count);
            var index = Interlocked.Decrement(ref count);
            if (index >= maxPlayer)
            {
                Interlocked.Decrement(ref _count);
                return false;
            }
            return true;
        }
        public bool isFull()
        {
            return _count >= maxPlayer;
        }

        public Player GetPlayer(int index)
        {
            players.TryGetValue(index, out var player);
            return player;
        }

        public void Remove(Player player)
        {
            if (players.TryRemove(player.Id, out var p))
            {
                Interlocked.Decrement(ref _count);
                player.Reset();
            }
        }
        public bool ContainPlayer(Player player)
        {
            return players.ContainsKey(player.Id);
        }
        public virtual void Stop()
        {
            foreach (var item in Players)
            {
                item.Reset();
            }
            players.Clear();
            Interlocked.Exchange(ref _count, 0);
            IsRunning = false;
            game.Stop();
        }
        public virtual void Start()
        {
            TryThrowNotStartException();
            IsRunning = true;
            foreach (var item in Players)
            {
                item.outlineable = startOut;
            }
            game.Start();
        }
        private void TryThrowNotStartException()
        {
            if (!isFull())
                throw new GameException("No enough player for start");
        }
    }
}
