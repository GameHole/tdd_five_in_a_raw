namespace Five
{
    class StopOutLineable : IOutLineable
    {
        private Game game;
        private Player player;
        public StopOutLineable(Game game,Player player)
        {
            this.game = game;
            this.player = player;
        }
        public void OutLine()
        {
            game.Remove(player);
        }
    }
}
