using UnityEngine;

namespace Five
{
    public abstract class AChessView: MonoBehaviour
    {
        private int _chessType;
        public int ChessType
        {
            get => _chessType;
            set
            {
                _chessType = value;
                TypeSetted();
            }
        }
        protected Color TypeToColor(float alpha)
        {
            var color = new Color();
            color.a = alpha;
            color.r = color.g = color.b = ChessType - 1;
            return color;
        }
        protected abstract void TypeSetted();
    }
}
