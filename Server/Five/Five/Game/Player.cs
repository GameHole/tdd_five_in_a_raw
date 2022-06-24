using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Player
    {
        public INotifier notifier;
        internal IPlayable playable;
        public int GameId { get; internal set; }
        public int chess { get; private set; }
        public int PlayerId { get; internal set; }
        public event Action onStart;
        public event Action onFinish;
        public Player()
        {
            Reset();
            notifier = new NoneNotifier();
        }
        public virtual void Start(int chess)
        {
            this.chess = chess;
            onStart?.Invoke();
        }
        public virtual Result Play(int x,int y)
        {
            return playable.Play(x, y, this);
        }
        internal void Reset()
        {
            GameId = 0;
            PlayerId = -1;
            playable = new NonePlayable();
            chess = 0;
        }

        public virtual void Finish()
        {
            Reset();
            onFinish?.Invoke();
        }
    }
}
