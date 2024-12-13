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

        public int RoomId { get; internal set; } = -1;
        public int Id { get; }
        public int state { get;private set; }

        public Player(int id=0)
        {
            Id = id;
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
          
        }
       
        public virtual void Reset()
        {
            RoomId = 0;
            state = StateDefine.Idle;
            playable = new NonePlayable();
            outlineable = new NoneOutLineable();
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

        public bool TrySwitchStateTo(int nextState)
        {
            if (forbinedTransition(nextState))
                return false;
            if (inviledTransition(nextState))
                return false;
            state = nextState;
            return true;
        }

        private bool inviledTransition(int nextState)
        {
            return nextState == state;
        }

        private bool forbinedTransition(int nextState)
        {
            return nextState == StateDefine.Matching 
                && state == StateDefine.Playing;
        }
    }
}
