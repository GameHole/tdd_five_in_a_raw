﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    [Serializable]
    public class ChessColorException:Exception
    {
        public ChessColorException(string msg) : base(msg) { }
    }
}
