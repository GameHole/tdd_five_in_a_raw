using UnityEngine;

namespace Five
{
    class CameraRay : IRay
    {
        Camera camera;
        public CameraRay(Camera camera)
        {
            this.camera = camera;
        }
        public Ray GetRay()
        {
            return camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
