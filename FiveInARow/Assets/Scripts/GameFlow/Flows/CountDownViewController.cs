namespace Five
{
    public class CountDownViewController : IGameFinish,IPlayerTurn,IGameStart
    {
        public CountDownView countView;
        public void Finish(int win)
        {
            countView.Close();
        }

        public void Start(int selfChess)
        {
            countView.Open();
        }

        public void Turn(int chess)
        {
            countView.Reset();
        }
    }
}
