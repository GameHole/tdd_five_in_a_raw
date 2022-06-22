using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class TableRangeException:Exception
    {
        public TableRangeException(string msg) : base(msg) { }
    }
}
