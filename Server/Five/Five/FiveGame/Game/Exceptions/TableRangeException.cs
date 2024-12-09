using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    [Serializable]
    public class TableRangeException:Exception
    {
        public TableRangeException(string msg) : base(msg) { }
    }
}
