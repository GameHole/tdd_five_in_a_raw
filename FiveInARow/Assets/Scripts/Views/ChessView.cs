using UnityEngine;
namespace Five
{
    public class ChessView : AChessView
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

        protected override void TypeSetted()
        {
            var mat = meshRenderer.material;
            mat.color = TypeToColor(mat.color.a);
        }
    }
}
