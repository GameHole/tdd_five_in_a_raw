using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Player
    {
        internal IPlayable playable;
        public int GameId { get; internal set; }
        public int chess { get; private set; }
        public int PlayerId { get; internal set; }
        public Player()
        {
            Reset();
        }
        public virtual void Match() { }

        public virtual void Start(int chess)
        {
            this.chess = chess;
        }

        public virtual void CancelMatch() { }

        public Result Play(int x,int y)
        {
            return playable.Play(x, y, this);
        }
        public void Reset()
        {
            GameId = 0;
            PlayerId = -1;
            playable = new NonePlayable();
            chess = 0;
        }
    }
}
