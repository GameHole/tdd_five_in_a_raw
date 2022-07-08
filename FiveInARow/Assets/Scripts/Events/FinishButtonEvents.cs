namespace Five
{
    public class FinishButtonEvents
    {
        public FinishButtonEvents(MatchView match, FinishView finish, GameView game)
        {
            finish.button.AddListener(() =>
            {
                match.Open();
                finish.Close();
                game.Close();
            });
        }
    }
}
