using System.Collections.Generic;
using UnityEngine;

namespace Five
{
    public class GradingView:AView
    {
        public int width { get; set; } = 15;
        public int height { get; set; } = 15;
        public List<Transform> griddingH { get; set; } = new List<Transform>();
        public List<Transform> griddingV { get; set; } = new List<Transform>();
        public float gradingWidth { get; private set; } = 0.015f;
        private Transform parent;
        public GradingView(int width, int height):base()
        {
            this.width = width;
            this.height = height;
            DrawGrading();
        }
        private void DrawGrading()
        {
            parent = View.transform.Find("Gradings");
            DrawV();
            DrawH();
        }
        private void Draw(int size, Vector3 scale, List<Transform> list, System.Func<int, Vector3> getPosition)
        {
            var prefab = PrefabHelper.Find("GameObjects/Grading");
            for (int i = 0; i < size; i++)
            {
                Vector3 position = getPosition(i);
                var grading = Object.Instantiate(prefab).transform;
                grading.SetParent(parent);
                grading.localScale = scale;
                grading.position = position;
                list.Add(grading);
            }
        }
        private void DrawV()
        {
            Draw(width+1, new Vector3(gradingWidth, 1, height), griddingV, (i) => new Vector3(i, 0, height*0.5f));
        }

        private void DrawH()
        {
            Draw(height+1, new Vector3(width, 1, gradingWidth), griddingH, (i) => new Vector3(width * 0.5f, 0, i));
        }

        protected override GameObject GenrateView()
        {
            return PrefabHelper.Instantiate<GameObject>("GameObjects/ChessBoard");
        }
    }
}
