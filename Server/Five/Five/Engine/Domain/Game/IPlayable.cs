using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public interface IPlayable
    {
        Result Commit(Message message,Player player);
    }
}
