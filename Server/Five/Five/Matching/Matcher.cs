using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Matcher
    {
        public Player Player { get; set; }
        public int GameId { get => Player.GameId; }

        public Matcher()
        {
            Player = new Player();
            Player.onStart += Started;
            Player.onFinish += Finished;
        }
        public virtual void Started()
        {
        }
        public virtual void Finished()
        {
        }
        public virtual void Matched()
        {
        }
        public virtual void Canceled()
        {
        }
    }
}
