using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Chessboard
    {
        public int width { get; private set; }
        public int height { get; private set; }
        public int Count { get => _count; }
        int _count;
        int[] table;
        AScaner[] scaners;
        public Chessboard(int width, int height)
        {
            if (width <= 5 || height <= 5)
                throw new TableRangeException("Table's width or height must be greater then five");
            this.width = width;
            this.height = height;
            table = new int[width * height];
            scaners = new AScaner[] 
            { 
                new HorizontalScaner(),new VerticalScaner(),new MinorDiagonalScaner(),new MainDiagonalScaner()
            };
            foreach (var item in scaners)
            {
                item.SetTable(this);
            }
        }

        public int GetValue(int x, int y)
        {
            TryThrowPositionOutOfRange(x, y);
            return table[toIndex(x, y)];
        }
        public int toIndex(int x, int y)
        {
            return x * width + y;
        }
        void TryThrowPositionOutOfRange(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                throw new ArgumentOutOfRangeException();
        }
        public bool AddValue(int x, int y, int color)
        {
            TryThrowPositionOutOfRange(x, y);
            TryThrowColorException(color);
            var index = toIndex(x, y);
            if (table[index] == 0)
            {
                table[index] = color;
                _count++;
                return true;
            }
            return false;
        }

        void TryThrowColorException(int color)
        {
            if (color == 0)
                throw new ChessColorException("Chess color must greater then zero");
        }

        public bool isFiveInRow(int color)
        {
            TryThrowColorException(color);
            for (int i = 0; i < scaners.Length; i++)
            {
                if (scaners[i].isContainFiveRow(color))
                    return true;
            }
            return false;
        }
    }
}
