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
            var w = width * 0.5f;
            var h = height * 0.5f;
            colider.transform.localScale = new Vector3(w+1, 0.001f, h+1);
            colider.transform.position = new Vector3(w, 0, h);
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
