﻿using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class LogPlayer : Player
    {
        public string log;
        public override void Match()
        {
            base.Match();
            log += "Match ";
        }
        public override void CancelMatch()
        {
            base.CancelMatch();
            log += "CancelMatch ";
        }
        public override void Start(int chess)
        {
            base.Start(chess);
            log += "Start ";
        }
        public override Result Play(int x, int y)
        {
            log += $"Play({x},{y}) ";
            return base.Play(x, y);
        }
        public override void Reset()
        {
            base.Reset();
            log += $"Reset ";
        }
        public override void OutLine()
        {
            base.OutLine();
            log += "OutLine ";
        }

        public static LogPlayer EmntyLog()
        {
            var player = new LogPlayer();
            player.log = "";
            return player;
        }
    }
}
