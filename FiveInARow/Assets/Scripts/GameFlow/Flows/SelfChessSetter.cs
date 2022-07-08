namespace Five
{
    public class SelfChessSetter : IGameStart
    {
        public SelfChessView self;
        public void Start(int selfChess)
        {
            self.View.GetComponent<AChessView>().ChessType = selfChess;
        }
    }
}
