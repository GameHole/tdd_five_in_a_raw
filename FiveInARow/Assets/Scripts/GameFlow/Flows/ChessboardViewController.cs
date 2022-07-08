using UnityEngine;

namespace Five
{
    public class ChessboardViewController : IPlayerPlay,IGameStart
    {
        public ChessboardView chessView;

        public void Play(int chess, Vector2Int pos)
        {
            chessView.SetChess(pos.x, pos.y, chess);
        }

        public void Start(int selfChess)
        {
            chessView.Clear();
        }
    }
}
