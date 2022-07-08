using Five;

namespace UnitTests
{
    internal class LogUpdater:IUpdate
    {
        internal bool isRun;

        public void Update(float dt)
        {
            isRun = true;
        }
    }
}