using System.Collections.Generic;
using UnityEngine;

namespace Five
{
    public class GradingView
    {
        public int width { get; set; } = 15;
        public int height { get; set; } = 15;
        public GameObject view { get; set; }
        public List<Transform> griddingH { get; set; } = new List<Transform>();
        public List<Transform> griddingV { get; set; } = new List<Transform>();
        public float gradingWidth { get; private set; } = 0.015f;
        public GradingView(int width, int height)
        {
            this.width = width;
            this.height = height;
            view = PrefabHelper.Instantiate<GameObject>("GameObjects/ChessBoard");
            DrawGrading();
        }
        private void DrawGrading()
        {
            var parent = view.transform.Find("Gradings");
            DrawV(parent);
            DrawH(parent);
        }
        private void Draw(Transform parent, Vector3 scale, List<Transform> list, System.Func<int, Vector3> getPosition)
        {
            var prefab = PrefabHelper.Find("GameObjects/Grading");
            for (int i = 0; i < width; i++)
            {
                Vector3 position = getPosition(i);
                var grading = Object.Instantiate(prefab).transform;
                grading.SetParent(parent);
                grading.localScale = scale;
                grading.position = position;
                list.Add(grading);
            }
        }
        private void DrawV(Transform parent)
        {
            Draw(parent, new Vector3(gradingWidth, 1, height), griddingV, (i) => new Vector3(i, 0, 0));
        }

        private void DrawH(Transform parent)
        {
            Draw(parent, new Vector3(width, 1, gradingWidth), griddingH, (i) => new Vector3(0, 0, i));
        }
    }
}
