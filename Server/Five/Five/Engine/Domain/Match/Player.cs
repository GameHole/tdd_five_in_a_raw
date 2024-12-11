using Five.RTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Player
    {
        internal IOutLineable outlineable;

        public INotifier notifier;
        internal IPlayable playable;

        public IRTSNotifier rtsnotifier;

        public int RoomId { get; internal set; } = -1;
        public int chess { get; private set; }
        public int PlayerId { get; internal set; }
        public Player()
        {
            Reset();
            notifier = new NoneNotifier();
            rtsnotifier = new NoneRTSNotifier();
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

        public virtual Result Commit(Message message)
        {
            return playable.Commit(message, this);
        }
    }
}
