using System;
using UnityEngine;

namespace Five
{
    public class GameView : AView
    {
        public GameView() : base() { }
        public GameView(Container container):this()
        {
            Canvas = View.transform.Find("Canvas");
            container.Set(this);
            var chessboard = container.Get<ChessboardView>();
            var finishView = container.Get<FinishView>();
            var countDown = container.Get<CountDownView>();
            var grading = container.Get<GradingView>();
            var chessSelector = container.Get<ChessSelectorView>();
            countDown.Join(Canvas);
            finishView.Join(Canvas);
            chessboard.View.transform.SetParent(View.transform);
            grading.View.transform.SetParent(View.transform);
            container.Get<SelfChessView>().View.transform.SetParent(View.transform);
            Close();
        }

        public Transform Canvas { get; }

        protected override GameObject GenrateView()
        {
            return PrefabHelper.Instantiate<GameObject>("GameObjects/GameView");
        }
    }
}
