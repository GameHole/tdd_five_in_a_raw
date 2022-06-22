using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class TimerDriver
    {
        private static Dictionary<LoopTimer, System.Timers.Timer> pairs = new Dictionary<LoopTimer, System.Timers.Timer>();

        public static void Clear()
        {
            foreach (var item in pairs.Keys)
            {
                Stop(item);
            }
            pairs.Clear();
        }

        public static int TimerCount { get=> pairs.Count; }

        public static LoopTimer New(float time)
        {
            var loopTimer = new LoopTimer(time);
            Start(loopTimer);
            return loopTimer;
        }

        public static bool IsContainTimer(LoopTimer timer)
        {
            return pairs.ContainsKey(timer);
        }

        public static void Stop(LoopTimer timer)
        {
            if(pairs.TryGetValue(timer,out var sysTimer))
            {
                sysTimer.Close();
                sysTimer.Dispose();
                pairs.Remove(timer);
            }
        }

        public static bool Start(LoopTimer loopTimer)
        {
            if (IsContainTimer(loopTimer))
                return false;
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (o, e) => loopTimer.Update(1);
            timer.Start();
            pairs.Add(loopTimer, timer);
            return true;
        }
    }
}
