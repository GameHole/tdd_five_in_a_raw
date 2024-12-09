using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public interface IPlayable
    {
        Result Play(int x, int y,Player player);
    }
}
