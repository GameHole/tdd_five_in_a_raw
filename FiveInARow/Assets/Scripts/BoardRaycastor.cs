using UnityEngine;

namespace Five
{
    public class BoardRaycastor
    {
        public PositionConvertor convertor { get; private set; }
        public Collider colider { get; private set; }

        public BoardRaycastor(float width,float height)
        {
            this.convertor = new PositionConvertor();
            colider = new GameObject().AddComponent<BoxCollider>();
            colider.transform.localScale = new Vector3(width, 0.001f, height);
            colider.transform.position = new Vector3(width, 0, height) *0.5f;
        }


        public bool Raycast(Ray ray, out Vector2Int pos)
        {
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                pos = convertor.ToRemotePosition(hit.point);
                return true;
            }
            pos = default;
            return false;
        }
    }
}
