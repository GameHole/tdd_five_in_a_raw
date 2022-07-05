using UnityEngine;

namespace Five
{
    class ChessSelectorActor : IChessSelectActor
    {
        private ChessSelectorView selector;
        private BoardRaycastor castor;
        internal GameObject chessPreview;
        internal Vector2Int remotePos;
        private IRay ray;
        public ChessSelectorActor(ChessSelectorView selector, IRay ray, BoardRaycastor castor)
        {
            this.selector = selector;
            this.ray = ray;
            this.castor = castor;
            chessPreview = PrefabHelper.Instantiate<GameObject>("GameObjects/ChessPreview");
        }

        public void Place()
        {
            selector.onPlace?.Invoke(remotePos);
        }

        public void Update()
        {
            bool active = castor.Raycast(ray.GetRay(), out remotePos);
            if (active)
            {
                chessPreview.transform.position = castor.convertor.ToLocalPosition(remotePos);
            }
            chessPreview.SetActive(active);
        }
    }
}
