using UnityEngine;
namespace Five
{
    public class ChessView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private MeshRenderer meshRenderer
        {
            get
            {
                if (!_meshRenderer)
                    _meshRenderer = GetComponentInChildren<MeshRenderer>();
                return _meshRenderer;
            }
        }
        public Color color { get => meshRenderer.material.color; }
        private int _chessType;
        public int ChessType
        {
            get => _chessType;
            set
            {
                _chessType = value;
                SetColor(gameObject, _chessType);
            }
        }
        void SetColor(GameObject clone, int chessType)
        {
            var mat = meshRenderer.material;
            var color = mat.color;
            color.r = color.g = color.b = chessType - 1;
            mat.color = color;
        }
    }
}
