using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class LoopTimer
    {
        public float time { get; set; }
        public float addingUpTime { get; private set; }
        public event Action onTime;

        public LoopTimer(float time)
        {
            this.time = time;
        }

        public void Update(float dt)
        {
            this.addingUpTime += dt;
            if(addingUpTime>=time)
            {
                addingUpTime = 0;
                onTime?.Invoke();
            }
        }
    }
}
