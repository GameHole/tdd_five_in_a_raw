using UnityEngine;

namespace Five
{
    public class GameBuilder
    {
        private float countDown;
        private int boardSize;
        private Camera camera;
        public GameBuilder(float countDown,int boardSize,Camera camera)
        {
            this.countDown = countDown;
            this.boardSize = boardSize;
            this.camera = camera;
        }
        public void Build(Container container)
        {
            container.Set(new ChessboardView(new PositionConvertor()));
            container.Set(new FinishView());
            container.Set(new CountDownView(countDown));
            container.Set(new GradingView(boardSize, boardSize));
            var selector = container.Set(new ChessSelectorView(new CameraRay(camera), new BoardRaycastor(boardSize, boardSize)));
            container.Set(new ChessPlacer(new MouseInput(), selector));
            container.Set(new SelfChessView());
        }
    }
}
