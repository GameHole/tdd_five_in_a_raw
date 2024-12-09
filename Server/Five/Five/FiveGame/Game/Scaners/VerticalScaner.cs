using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class VerticalScaner : AScaner
    {
        protected override int maxX => table.width;
        protected override int maxY => table.height - 5;

        protected override int combineX(int x, int idx)
        {
            return x;
        }
        protected override int combineY(int y,int idx)
        {
            return y + idx;
        }
    }
}
