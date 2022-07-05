using System;
using UnityEngine;

namespace Five
{
    public class GameView : AView
    {
        public ChessboardView chessboard;
        public FinishView finishView;
        public CountDownView countDown;
        public ChessSelectorView chessSelector;
        public GradingView grading;
        public GameView()
        {
            Canvas = View.transform.Find("Canvas");
            chessboard = new ChessboardView(new PositionConvertor());
            finishView = new FinishView();
            countDown = new CountDownView(15);
            grading = new GradingView(15, 15);
            chessSelector = new ChessSelectorView(new CameraRay(Camera.main), new BoardRaycastor(15,15));
            countDown.Join(Canvas);
            finishView.Join(Canvas);
            chessboard.View.transform.SetParent(View.transform);
            grading.View.transform.SetParent(View.transform);
            Close();
        }

        public Transform Canvas { get; }

        protected override GameObject GenrateView()
        {
            return new GameObject();
        }
    }
}
