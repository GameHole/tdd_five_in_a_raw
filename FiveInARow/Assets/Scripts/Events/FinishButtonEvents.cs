namespace Five
{
    public class FinishButtonEvents
    {
        public FinishButtonEvents(MatchView match, FinishView finish, GameView game)
        {
            finish.button.onClick.AddListener(() =>
            {
                match.Open();
                finish.Close();
                game.Close();
            });
        }
    }
}
