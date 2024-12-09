using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Turn
    {
        private int pointCount;
        public event Action<object> onTurn;
        private int _index;
        public int index
        {
            get => _index; 
            set
            {
                _index = value;
                onTurn?.Invoke(_index);
            }
        }

        public Turn(int pointCount)
        {
            this.pointCount = pointCount;
        }

        public void Start()
        {
            index = 0;
        }

        public void Next()
        {
            index = (index + 1) % pointCount;
        }
    }
}
