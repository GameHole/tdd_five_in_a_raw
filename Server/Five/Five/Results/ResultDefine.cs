using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public static class ResultDefine
    {
        public static readonly Result Success = new Result(0);

        public static readonly Result PlayerNotInTheGame = new Result(20010);


        public static readonly Result GameNotStart = new Result(30000);
        public static readonly Result PlayerIsNotThis = new Result(30001);
        public static readonly Result AllReadyHasChess = new Result(30002);
    }
}
