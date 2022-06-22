using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Player
    {
        public Game game { get; internal set; }
        //public int GameId { get; internal set; }
        public int chess { get; private set; }
        public int PlayerId { get; internal set; }
        public Player()
        {
            Reset();
        }
        public virtual void Match() { }

        public virtual void Start(Game game, int chess)
        {
            this.chess = chess;
            this.game = game;
        }

        public virtual void CancelMatch() { }

        public Result Play(int x,int y)
        {
            if (game == null)
            {
                return ResultDefine.PlayerNotInTheGame;
            }
            return game.Play(x, y, this);
        }

        public void Finish()
        {
            Reset();
            chess = 0;
        }

        public void Reset()
        {
            game = null;
            PlayerId = -1;
        }
    }
}
