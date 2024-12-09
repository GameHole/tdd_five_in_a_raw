using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Five
{
    public class LoopTimer
    {
        public float time { get; set; }
        private float _adding;
        public float addingUpTime { get => _adding; }
        public event Action onTime;

        public LoopTimer(float time)
        {
            this.time = time;
        }

        public void Update(float dt)
        {
            Interlocked.Exchange(ref _adding, _adding + dt);
            if (_adding >= time)
            {
                Reset();
                onTime?.Invoke();
            }
        }
        public void Reset()
        {
            Interlocked.Exchange(ref _adding, 0);
        }
    }
}
