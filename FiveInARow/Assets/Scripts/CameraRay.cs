using UnityEngine;

namespace Five
{
    public class CameraRay : IRay
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
