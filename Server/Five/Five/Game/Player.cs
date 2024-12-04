using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Player
    {
        public INotifier notifier;
        internal IPlayable playable;
        internal IOutLineable outlineable;
        public int RoomId { get; internal set; } = -1;
        public int chess { get; private set; }
        public int PlayerId { get; internal set; }
        public Player()
        {
            Reset();
            notifier = new NoneNotifier();
        }
        public virtual void Match()
        {

        }
        public virtual void CancelMatch()
        {

        }
        public virtual void Start(int chess)
        {
            this.chess = chess;
        }
        public virtual Result Play(int x,int y)
        {
            return playable.Play(x, y, this);
        }
        public virtual void Reset()
        {
            RoomId = 0;
            PlayerId = -1;
            playable = new NonePlayable();
            outlineable = new NoneOutLineable();
            chess = 0;
        }

        public virtual void OutLine()
        {
            notifier = new NoneNotifier();
            outlineable.OutLine();
        }
    }
}
