using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public static class ResultDefine
    {
        public static readonly Result Success = new Result(0);

        public static readonly Result Matching = new Result(20000);
        public static readonly Result GameStarted = new Result(20001);
        public static readonly Result NotInMatching = new Result(20002);

        public static readonly Result PlayerNotInTheGame = new Result(29999);
        public static readonly Result GameNotStart = new Result(30000);
        public static readonly Result NotCurrentTurnPlayer = new Result(30001);
        public static readonly Result AllReadyHasChess = new Result(30002);
    }
}
