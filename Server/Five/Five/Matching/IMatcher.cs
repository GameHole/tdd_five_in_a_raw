using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public interface IMatchable
    {
        Result Match(Matcher player);
        Result Cancel(Matcher player);
    }
}
