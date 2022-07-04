using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public static class MessageCode
    {
        public static readonly int RequestMatch = 1;
        public static readonly int RequestCancelMatch = 3;
        public static readonly int RequestPlay = 5;
        public static readonly int StartNotify = 7;
        public static readonly int PlayedNotify = 9;
        public static readonly int FinishNotify = 11;
        public static readonly int TurnNotify = 13;
        public static int GetResponseCode(int reqCode) => reqCode + 1;
    }
}
