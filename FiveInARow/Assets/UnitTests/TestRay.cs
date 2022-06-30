using Five;
using UnityEngine;

namespace UnitTests
{
    class TestRay : IRay
    {
        public Ray GetRay()
        {
            return new Ray(new Vector3(0.5f, 1, 0.5f), Vector3.down);
        }
    }
}
