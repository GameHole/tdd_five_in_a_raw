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
        private bool isCasting;
        public ChessSelectorActor(ChessSelectorView selector, IRay ray, BoardRaycastor castor)
        {
            this.selector = selector;
            this.ray = ray;
            this.castor = castor;
            chessPreview = PrefabHelper.Instantiate<GameObject>("GameObjects/ChessPreview");
        }

        public void Place()
        {
            if (isCasting)
                selector.onPlace?.Invoke(remotePos);
        }

        public void Update()
        {
            isCasting = castor.Raycast(ray.GetRay(), out remotePos);
            if (isCasting)
            {
                chessPreview.transform.position = castor.convertor.ToLocalPosition(remotePos);
            }
            chessPreview.SetActive(isCasting);
        }
    }
}
