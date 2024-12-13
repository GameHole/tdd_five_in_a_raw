using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSCharater: Charater
    {
        public float x;
        public float y;
        public float targetX;
        public float targetY;

        public RTSCharater(int id) : base(id) { }
    }
}
