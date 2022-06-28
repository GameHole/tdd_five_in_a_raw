using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequest : PlayMessage
    {
        public PlayRequest(int x,int y) : base(MessageCode.RequestPlay,x,y)
        {

        }
    }
}
