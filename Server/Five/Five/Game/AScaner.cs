namespace Five
{
    abstract class AScaner
    {
        protected Chessboard table { get; private set; }
        public void SetTable(Chessboard table)
        {
            this.table = table;
        }
        public bool isContainFiveRow(int color)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (isEqualOfFive(color, y, x))
                        return true;
                }
            }
            return false;
        }    
        protected bool isEqualOfFive(int color, int y, int x)
        {
            for (int i = 0; i < 5 - 1; i++)
            {
                var cur = table.GetValue(combineX(x, i), combineY(y, i));
                var next = table.GetValue(combineX(x, i + 1), combineY(y, i + 1));
                if (isNotEqual(color, cur, next))
                    return false;
            }
            return true;
        }
        protected bool isNotEqual(int color, int cur, int next)
        {
            return cur != color || next != color || cur != next;
        }
        protected abstract int maxX { get; }
        protected abstract int maxY { get; }
        protected abstract int combineX(int x, int idx);
        protected abstract int combineY(int y, int idx);
    }
}