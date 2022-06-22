using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class HorizontalScaner : AScaner
    {

        protected override int maxX => table.width - 5;
        protected override int maxY => table.height;

        protected override int combineX(int x, int idx)
        {
            return x + idx;
        }

        protected override int combineY(int y, int idx)
        {
            return y;
        }
    }
}
