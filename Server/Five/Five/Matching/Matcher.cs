using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Matcher
    {
        public Player Player { get; private set; }
        private IMatchable matchable;
        Dictionary<Type, IMatchable> kv = new Dictionary<Type, IMatchable>();
        void Add<T>(T item) where T : IMatchable
        {
            kv.Add(typeof(T), item);
        }
        public void Set<T>() where T : IMatchable
        {
            matchable = kv[typeof(T)];
        }
        public int GameId { get => Player.GameId; }

        public Matcher(Matching matching)
        {
            Add(new DefaultMatcher(matching));
            Add(new MachingMatcher(matching));
            Add(new GameStartedMatcher());
            Reset();
            Player = new Player();
            Player.onStart += Started;
            Player.onFinish += Finished;
        }
        public Result Match()
        {
            return matchable.Match(this);
        }
        public Result Cancel()
        {
            return matchable.Cancel(this);
        }
        public virtual void Started()
        {
            Set<GameStartedMatcher>();
        }
        private void Reset()
        {
            Set<DefaultMatcher>();
        }
        public virtual void Finished()
        {
            Reset();
        }
        public virtual void Matched()
        {
        }
        public virtual void Canceled()
        {
        }
    }
}
