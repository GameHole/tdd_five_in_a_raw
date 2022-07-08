using System.Threading;
using UnityEngine;
namespace UnitTests
{
    internal class ThreadIdTaker:MonoBehaviour
    {
        internal int threadId;

        private void Start()
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}