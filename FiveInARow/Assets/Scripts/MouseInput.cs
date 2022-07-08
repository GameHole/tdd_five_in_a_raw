using UnityEngine;
namespace Five
{
    class MouseInput : IInput
    {
        public bool GetDown()
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}
