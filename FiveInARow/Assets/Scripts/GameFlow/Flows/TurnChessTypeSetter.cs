using System.Collections.Generic;
using UnityEngine;

namespace Five
{
    public class TurnChessTypeSetter:IPlayerTurn
    {
        List<AChessView> views = new List<AChessView>();
        public TurnChessTypeSetter Add(GameObject game)
        {
            views.Add(game.GetComponent<AChessView>());
            return this;
        }

        public void Turn(int chess)
        {
            foreach (var item in views)
            {
                item.ChessType = chess;
            }
        }
    }
}
