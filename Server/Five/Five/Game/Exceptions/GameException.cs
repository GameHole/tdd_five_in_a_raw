using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class GameException:Exception
    {
        public GameException(string msg) : base(msg) { }
    }
}
